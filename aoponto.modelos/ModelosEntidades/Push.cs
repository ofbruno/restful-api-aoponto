using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace aoponto.modelos
{
    [Table("Pushes")]
    public class Push
    {
        public int Id { get; set; }
        public int NotificacaoId { get; set; }
        public DateTime DataEnvio { get; set; }
        public int StatusEnvio { get; set; }
        public string MensagemRetorno { get; set; }
        public string Destinatario { get; set; }

        public Notificacao Notificacao { get; set; }
    }
}
