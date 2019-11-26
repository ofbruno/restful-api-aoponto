using System;
using System.Collections.Generic;
using System.Linq;
using aoponto.dados;
using aoponto.modelos;

namespace aoponto.servicos
{
    public class OfertaLoteServico : BaseServico<Lote>, IOfertaLoteServico
    {
        INotificacaoServico _notificacaoServico;
        IPushServico _pushServico;
        ILoteDao _loteDao;

        public OfertaLoteServico(AppDbContext contexto, IIdentificacao identificacao, INotificacaoServico notificacaoServico, IPushServico pushServico, ILoteDao loteDao) : base(contexto, identificacao)
        {
            _notificacaoServico = notificacaoServico;
            _pushServico = pushServico;
            _loteDao = loteDao;
        }

        public Retorno Obter(int id)
        {
            var lote = this.GetOne(id, "Endereco", "Etapas", "Endereco.Estado", "Endereco.Cidade");
                        
            if (lote == null)
            {
                return Falha(ETipoFalha.RegistroNaoEncontrado);
            }
            else
            {
                return Sucesso((ViewLoteConsulta)lote);
            }
        }

        public Retorno Atualizar(Lote lote)
        {
            return Falha("Não disponível");
        }

        public Retorno Cancelar(int id)
        {
            return Falha("Não disponível");
        }

        public Retorno Inserir(ViewLote viewLote)
        {
            try
            {                
                var lote = viewLote.ObterLoteInclusao(this.Identificacao.Id);
                var alterarEndereco = false;
                var endereco = null as Endereco;
                var notificacao = null as Notificacao;
                var retorno = null as Retorno;

                if (lote.Endereco.Id > 0)
                {
                    endereco = this.GetOne<Endereco>(lote.Endereco.Id);
                        
                    if (endereco == null)
                    {
                        lote.Endereco.Id = 0;
                        lote.EnderecoId = 0;
                        lote.Endereco.UsuarioId = lote.UsuarioIdVendedor;
                    }
                    else
                    {
                        alterarEndereco = true;

                        endereco.Nome = lote.Endereco.Nome;
                        endereco.Logradouro = lote.Endereco.Logradouro;
                        endereco.Numero = lote.Endereco.Numero;
                        endereco.Complemento = lote.Endereco.Complemento;
                        endereco.CidadeId = lote.Endereco.CidadeId;
                        endereco.EstadoId = lote.Endereco.EstadoId;
                        endereco.Coordenadas = lote.Endereco.Coordenadas;
                        endereco.CoordenadasEndereco = lote.Endereco.CoordenadasEndereco;

                        lote.EnderecoId = endereco.Id;
                        lote.Endereco = null;
                    }                        
                }


                if (alterarEndereco)
                {
                    this.Update<Endereco>(endereco);
                }

                this.Insert<Lote>(lote);
                
                if (lote.Tipo == (int)ETipoLote.Frigorificos)
                {
                    retorno = _notificacaoServico.CriarNotificacaoNovaOfertaLoteParaFrigorificos(lote);
                }
                else
                {
                    retorno = _notificacaoServico.CriarNotificacaoNovaOfertaLoteParaProdutores(lote);
                }

                if (retorno.Sucesso)
                {
                    notificacao = retorno.ObterDados<Notificacao>();

                    if (notificacao != null)
                    {
                        this.Insert<Notificacao>(notificacao);                            
                    }
                }

                this.Save();


                if (notificacao != null && notificacao.EnviaPush)
                {
                    _pushServico.Enviar(notificacao, false);
                }


                return Sucesso((ViewLote)lote, "Oferta do lote realizada com sucesso.");                                
            }
            catch (Exception ex)
            {
                return Erro(ex);
            }
        }

        public Retorno SelecionarLoteOfertado(int loteId)
        {
            if (Identificacao.Tipo != (int)ETipoUsuario.Frigorifico)
            {
                return Falha(ETipoFalha.NaoPermitido);
            }

            var lote = _contexto.Lotes.FirstOrDefault(x => x.Id == loteId);

            if (lote == null)
            {
                return Falha(ETipoFalha.RegistroNaoEncontrado);
            }

            if (lote.Status != (int)EStatusLote.Ofertado)
            {
                return Falha("Este lote não pode ser selecionado.");
            }

            lote.Status = (int)EStatusLote.SelecionadoParaCompra;

            var loteEtapa = new LoteEtapa()
            {
                LoteId = lote.Id,
                Status = (int)EStatusLote.SelecionadoParaCompra,
                UsuarioId = Identificacao.Id,
                DataHora = DateTime.Now
            };

            this.Update<Lote>(lote);
            this.Insert<LoteEtapa>(loteEtapa);
            this.Save();

            return new Sucesso("Lote selecionado com sucesso.");
        }

        public Retorno ListarOfertasDisponiveis()
        {
            try
            {
                var lotes = _loteDao.ListarOfertasDisponiveis(Identificacao.Id);
                var views = new List<ViewOfertaConsulta>();

                lotes.ForEach(x => { views.Add((ViewOfertaConsulta)x); });

                return Sucesso(views);
            }
            catch (Exception ex)
            {
                return Erro(ex);
            }
        }

        public Retorno ListarComprasLote()
        {
            try
            {
                var lotes = _loteDao.ListarComprasUsuario(Identificacao.Id);
                var views = new List<ViewLoteCompra>();

                lotes.ForEach(x => { views.Add((ViewLoteCompra)x); });
                
                return Sucesso(views);
            }
            catch (Exception ex)
            {
                return Erro(ex);
            }
        }

        public Retorno ListarVendasLote()
        {
            try
            {
                var lotes = _loteDao.ListarVendasUsuario(Identificacao.Id);
                var views = new List<ViewLoteVenda>();

                lotes.ForEach(x => { views.Add((ViewLoteVenda)x); });

                return Sucesso(views);
            }
            catch (Exception ex)
            {
                return Erro(ex);
            }
        }

        public Retorno ConsultarDetalhesVenda(int loteId)
        {
            try
            {
                var lote = _loteDao.Obter(loteId);
                var etapas = _loteDao.ListarEtapasLote(loteId);
                var viewLoteDetalhes = (ViewLoteDetalhes)lote;

                etapas.OrderBy(x => x.DataHora).ToList().ForEach(x => { viewLoteDetalhes.EtapasVenda.Add((ViewLoteEtapaVenda)x); });

                return Sucesso(viewLoteDetalhes);
            }
            catch (Exception ex)
            {
                return Erro(ex);
            }
        }

        public Retorno ConsultarDetalhesCompra(int loteId)
        {
            try
            {
                var lote = _loteDao.Obter(loteId);
                var etapas = _loteDao.ListarEtapasLote(loteId);
                var viewLoteDetalhes = (ViewLoteDetalhes)lote;

                etapas.OrderBy(x => x.DataHora).ToList().ForEach(x => { viewLoteDetalhes.EtapasCompra.Add((ViewLoteEtapaCompra)x); });

                return Sucesso(viewLoteDetalhes);
            }
            catch (Exception ex)
            {
                return Erro(ex);
            }
        }
        
    }
}
