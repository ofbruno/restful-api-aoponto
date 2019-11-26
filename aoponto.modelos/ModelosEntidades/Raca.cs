using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace aoponto.modelos
{
    [Table("Racas")]
    public class Raca
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public bool Ativo { get; set; }
    }
}
