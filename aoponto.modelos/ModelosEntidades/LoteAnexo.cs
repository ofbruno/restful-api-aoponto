using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace aoponto.modelos
{
    [Table("LoteAnexos")]
    public partial class LoteAnexo
    {
        public int Id { get; set; }
        public int LoteId { get; set; }
        public string Url { get; set; }

        public Lote Lote { get; set; }
    }
}
