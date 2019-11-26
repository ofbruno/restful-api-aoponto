using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace aoponto.modelos
{
    [Table("LoteRegistros")]
    public partial class LoteRegistro
    {
        public int Id { get; set; }
        public int LoteId { get; set; }
        public string Registro { get; set; }

        public Lote Lote { get; set; }
    }
}
