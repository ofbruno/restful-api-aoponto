using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace aoponto.modelos
{
    [Table("Produtores")]
    public class Produtor
    {
        public int Id { get; set; }

        public int UsuarioId { get; set; }

        public bool Comprador { get; set; }

        public Usuario Usuario { get; set; }

        //public List<Lote> Lotes { get; set; }

    }
}
