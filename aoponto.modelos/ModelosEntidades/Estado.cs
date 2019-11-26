using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace aoponto.modelos
{
    [Table("Estados")]
    public class Estado
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public string Sigla { get; set; }

    }
}
