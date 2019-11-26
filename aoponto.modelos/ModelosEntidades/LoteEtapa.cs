using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace aoponto.modelos
{
    [Table("LoteEtapas")]
    public partial class LoteEtapa
    {
        public int Id { get; set; }
        public int LoteId { get; set; }
        public int Status { get; set; }
        public int UsuarioId { get; set; }
        public DateTime DataHora { get; set; }

        public Lote Lote { get; set; }
        public Usuario Usuario { get; set; }
    }
}
