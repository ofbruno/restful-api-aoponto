using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using aoponto.lib;

namespace aoponto.modelos
{

    public class ViewFeed
    {
        public int Id { get; set; }
        public int TipoFeed { get; set; }
        public string DescricaoTipo { get; set; }
        public DateTime DataHora { get; set; }
        public string ImagemUrl { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public string Parametros { get; set; }
        public int TipoNotificacao { get; set; }
        public string DescricaoTempo { get { return Util.GerarDescricaoTempo(this.DataHora); } }

        public static explicit operator ViewFeed(ViewNoticia noticia)
        {
            var view = new ViewFeed()
            {
                TipoFeed = (int)ETipoFeed.Noticia,
                DescricaoTipo = ETipoFeed.Noticia.Descricao(),
                Id = noticia.Id,
                DataHora = noticia.DataHora,
                Titulo = noticia.Titulo,
                Descricao = noticia.Descricao,
                ImagemUrl = noticia.ImagemUrl
            };

            if (view.Descricao.Length > 230)
            {
                view.Descricao = view.Descricao.Substring(0, 230) + "...";
            }

            return view;
        }

        public static explicit operator ViewFeed(ViewNotificacao notificacao)
        {
            var view = new ViewFeed()
            {
                TipoFeed = (int)ETipoFeed.Notificacao,
                DescricaoTipo = ETipoFeed.Notificacao.Descricao(),
                Id = notificacao.Id,
                DataHora = notificacao.DataHora,
                Titulo = notificacao.Titulo,
                Descricao = notificacao.Mensagem,
                Parametros = notificacao.Parametros,
                TipoNotificacao = notificacao.Tipo
            };

            return view;
        }
    }
}
