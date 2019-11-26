using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using aoponto.dados;
using aoponto.modelos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace aoponto.servicos
{
    public class NotificacaoServico : BaseServico<Notificacao>, INotificacaoServico
    {
        INotificacaoDao _notificacaoDao;

        public NotificacaoServico(AppDbContext contexto, IIdentificacao identificacao, INotificacaoDao notificacaoDao) : base (contexto, identificacao)
        {
            _notificacaoDao = notificacaoDao;
        }

        public Retorno ObterListaPorUsuario(int quantidade = 0, int limiteIdSuperior = 0, int limiteIdInferior = 0)
        {
            if (quantidade == 0)
            {
                quantidade = 10;
            }

            try
            {
                var views = new List<ViewNotificacao>();
                var notificacoes = _notificacaoDao.ListarPorUsuario(this.Identificacao.Id, this.Identificacao.Tipo, quantidade, limiteIdSuperior, limiteIdInferior);

                notificacoes.ForEach(n => {
                    views.Add((ViewNotificacao)n);
                });

                return Sucesso(views);
            }
            catch (Exception ex)
            {
                return Erro(ex);
            }
        }

        public Retorno MarcarComoLido(int id)
        {
            try
            {
                var notificacao = _notificacaoDao.Obter(id);

                if (notificacao == null)
                {
                    return Falha(ETipoFalha.RegistroNaoEncontrado);
                }

                var dados = _notificacaoDao.ObterDadosLeitura(id, this.Identificacao.Id);

                if (dados == null)
                {
                    dados = new NotificacaoUsuario()
                    {
                        NotificacaoId = id,
                        UsuarioId = this.Identificacao.Id,
                        DataHoraLeitura = DateTime.Now
                    };

                    this.Insert(dados);
                }
                else
                {
                    dados.DataHoraLeitura = DateTime.Now;
                    this.Insert(dados);
                }

                this.Save();

                return Sucesso();
            }
            catch (Exception ex)
            {
                return Erro(ex);
            }
        }


        public Retorno CriarNotificacaoNovaOfertaLoteParaFrigorificos(Lote lote)
        {
            var parametros = new
            {
                tipo = (int)ETipoNotificacao.NovaOfertaLoteParaFrigorificos,
                loteId = lote.Id
            };

            var notificacao = new Notificacao()
            {
                Titulo = ETipoNotificacao.NovaOfertaLoteParaFrigorificos.TituloNotificacao(),
                Mensagem = ETipoNotificacao.NovaOfertaLoteParaFrigorificos.MensagemNotificacao(),
                Tipo = (int)ETipoNotificacao.NovaOfertaLoteParaFrigorificos,
                Parametros = Newtonsoft.Json.JsonConvert.SerializeObject(parametros),
                DataHora = DateTime.Now,
                EnviaPush = true,
                TipoDestino = (int)ETipoDestinoNotificacao.Individual,
                TipoUsuarioDestino = 0,
                NotificacoesUsuarios = new List<NotificacaoUsuario>()
            };

            var frigorificos = this.GetAll<Frigorifico>().ToList();

            frigorificos.ForEach(u => {
                notificacao.NotificacoesUsuarios.Add(new NotificacaoUsuario()
                {
                    UsuarioId = u.UsuarioId
                });
            });
            

            return Sucesso(notificacao);
        }

        public Retorno CriarNotificacaoNovaOfertaLoteParaProdutores(Lote lote)
        {
            var parametros = new
            {
                tipo = (int)ETipoNotificacao.NovaOfertaLoteParaProdutores,
                loteId = lote.Id
            };

            var notificacao = new Notificacao()
            {
                Titulo = ETipoNotificacao.NovaOfertaLoteParaProdutores.TituloNotificacao(),
                Mensagem = ETipoNotificacao.NovaOfertaLoteParaProdutores.MensagemNotificacao(),
                Tipo = (int)ETipoNotificacao.NovaOfertaLoteParaProdutores,
                Parametros = Newtonsoft.Json.JsonConvert.SerializeObject(parametros),
                DataHora = DateTime.Now,
                EnviaPush = true,
                TipoDestino = (int)ETipoDestinoNotificacao.Individual,
                TipoUsuarioDestino = 0,
                NotificacoesUsuarios = new List<NotificacaoUsuario>()
            };

            var produtores = this.GetAll<Produtor>().ToList();

            produtores.ForEach(u => {
                notificacao.NotificacoesUsuarios.Add(new NotificacaoUsuario()
                {
                    UsuarioId = u.UsuarioId
                });
            });

            return Sucesso(notificacao);
        }
    }
}
