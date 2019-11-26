using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace aoponto.modelos
{
    [Table("Notificacoes")]
    public partial class Notificacao
    {
        public int Id { get; set; }
        public int Tipo { get; set; }
        public string Titulo { get; set; }
        public string Mensagem { get; set; }
        public DateTime DataHora { get; set; }
        public bool EnviaPush { get; set; }
        public string Parametros { get; set; }
        public int TipoDestino { get; set; }
        public int TipoUsuarioDestino { get; set; }

        public List<NotificacaoUsuario> NotificacoesUsuarios { get; set; }
        public List<Push> Pushes { get; set; }
    }
}
