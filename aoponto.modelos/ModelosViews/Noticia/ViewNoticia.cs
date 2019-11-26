using System;
using System.Collections.Generic;
using System.Text;
using aoponto.lib;

namespace aoponto.modelos
{
    public class ViewNoticia
    {
        public int Id { get; set; }
        public DateTime DataHora { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public string Conteudo { get; set; }
        public string ImagemUrl { get; set; }
        public string FonteNome { get; set; }
        public string FonteLink { get; set; }
        public string DescricaoTempo { get { return Util.GerarDescricaoTempo(this.DataHora); } }

        public static explicit operator ViewNoticia(Noticia noticia)
        {
            var view = new ViewNoticia()
            {
                Id = noticia.Id,
                DataHora = noticia.DataHora,
                Titulo = noticia.Titulo,
                Descricao = noticia.Descricao,
                Conteudo = noticia.Conteudo,
                ImagemUrl = noticia.ImagemUrl,
                FonteNome = noticia.FonteNome,
                FonteLink = noticia.FonteLink
            };

            return view;
        }
    }
}
