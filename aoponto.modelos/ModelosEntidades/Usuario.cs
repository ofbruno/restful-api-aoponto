using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace aoponto.modelos
{
    [Table("Usuarios")]
    public class Usuario
    {
        public int Id { get; set; }

        [Required]
        public string FirebaseId { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public int Tipo { get; set; }

        [Required]
        public bool Ativo { get; set; }

        public DateTime DataHoraCadastro { get; set; }

        public DateTime DataHoraUltimoLogin { get; set; }

        public DateTime DataHoraUltimoAcesso { get; set; }
        
        public int Token { get; set; }

        public string FotoUrl { get; set; }

        public string Telefone { get; set; }


    }
}
