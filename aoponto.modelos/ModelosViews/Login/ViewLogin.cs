using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace aoponto.modelos
{

    public class ViewLogin
    {
        [Required]
        public string FirebaseId { get; set; }

        public string FotoUrl { get; set; }

        public string Telefone { get; set; }

        public string Nome { get; set; }

        [Required]
        public string Email { get; set; }

        public bool EmailVerificado { get; set; }

        public bool UsuarioNovo { get; set; }

        public string TipoOperacao { get; set; }

        public int TipoUsuario { get; set; }

    }
}
