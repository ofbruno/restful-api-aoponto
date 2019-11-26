using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using aoponto.dados;
using aoponto.modelos;

namespace aoponto.servicos
{
    public class HomeServico : BaseServico<Usuario>, IHomeServico
    {
        INoticiaServico _noticiaServico;
        INotificacaoServico _notificacaoServico;

        public HomeServico(AppDbContext contexto, IIdentificacao identificacao, INoticiaServico noticiaServico, INotificacaoServico notificacaoServico) : base (contexto, identificacao)
        {
            _noticiaServico = noticiaServico;
            _notificacaoServico = notificacaoServico;
        }

        public Retorno ListarFeed(int quantidade = 0, int limiteIdSuperior = 0, int limiteIdInferior = 0)
        {
            try
            {
                var retornoNoticias = _noticiaServico.Listar(quantidade, limiteIdInferior, limiteIdSuperior);
                var retornoNotificacoes = _notificacaoServico.ObterListaPorUsuario(quantidade, limiteIdSuperior, limiteIdInferior);
                var feed = new List<ViewFeed>();

                if (retornoNoticias.Sucesso)
                {
                    var noticias = retornoNoticias.ObterDados<List<ViewNoticia>>();

                    if (noticias != null)
                    {
                        noticias.ForEach(x => feed.Add((ViewFeed)x));
                    }                    
                }

                if (retornoNotificacoes.Sucesso)
                {
                    var notificacoes = retornoNoticias.ObterDados<List<ViewNotificacao>>();

                    if (notificacoes != null)
                    {
                        notificacoes.ForEach(x => feed.Add((ViewFeed)x));
                    }
                }

                feed = feed.OrderByDescending(x => x.DataHora).ToList();

                return Sucesso(feed);                                
            }
            catch (Exception ex)
            {
                return Erro(ex);
            }
            
        }
    }
}
