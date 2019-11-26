using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using aoponto.lib;

namespace aoponto.modelos
{

    public class ViewPerfil
    {
        public int Id { get; set; }
        public string FirebaseId { get; set; }
        public int Token { get; set; }
        public string FotoUrl { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public int Tipo { get; set; }
        public string DescricaoTipo { get; set; }
        public bool Comprador { get; set; }
        public string Telefone { get; set; }


        public static explicit operator ViewPerfil(Usuario usuario)
        {
            return new ViewPerfil()
            {
                Id = usuario.Id,
                FirebaseId = usuario.FirebaseId,
                Token = usuario.Token,
                FotoUrl = usuario.FotoUrl,
                Nome = usuario.Nome,
                Email = usuario.Email,
                Tipo = usuario.Tipo,
                Telefone = usuario.Telefone,
                DescricaoTipo = ((ETipoUsuario)usuario.Tipo).Descricao()
            };
        }
    }
}
