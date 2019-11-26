using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace aoponto.modelos
{
    [Table("Aparelhos")]
    public class Aparelho
    {
        public int Id { get; set; }

        public int UsuarioId { get; set; }

        public string Uuid { get; set; }

        public string Modelo { get; set; }

        public string TokenPush { get; set; }

        public Usuario Usuario { get; set; }

    }
}
