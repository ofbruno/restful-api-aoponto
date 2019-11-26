using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace aoponto.modelos
{
    [Table("Noticias")]
    public partial class Noticia
    {
        public int Id { get; set; }
        public DateTime DataHora { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public string Conteudo { get; set; }
        public string ImagemUrl { get; set; }
        public string FonteNome { get; set; }
        public string FonteLink { get; set; }
    }
}
