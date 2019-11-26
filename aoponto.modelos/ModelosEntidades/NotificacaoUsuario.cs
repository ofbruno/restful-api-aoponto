using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace aoponto.modelos
{
    [Table("NotificacoesUsuarios")]
    public class NotificacaoUsuario
    {
        public int Id { get; set; }

        public int NotificacaoId { get; set; }

        public int UsuarioId { get; set; }

        public DateTime DataHoraLeitura { get; set; }

        public Notificacao Notificacao { get; set; }

        public Usuario Usuario { get; set; }

    }
}
