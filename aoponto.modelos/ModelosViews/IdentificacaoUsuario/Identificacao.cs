using System;
using System.Collections.Generic;
using System.Text;

namespace aoponto.modelos
{
    public interface IIdentificacao
    {
        int Id { get; set; }
        int Tipo { get; set; }
        int Token { get; set; }
        string Nome { get; set; }
        string Email { get; set; }
    }

    public class Identificacao : IIdentificacao
    {
        public int Id { get; set; }
        public int Tipo { get; set; }
        public int Token { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
    }
}
