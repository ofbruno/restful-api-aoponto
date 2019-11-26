using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace aoponto.modelos
{
    [Table("Cidades")]
    public class Cidade
    {
        public int Id { get; set; }

        public int EstadoId { get; set; }

        public string Nome { get; set; }

        public Estado Estado { get; set; }

    }
}
