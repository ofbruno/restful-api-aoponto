using System;
using System.Collections.Generic;
using System.Text;

namespace aoponto.servicos
{
    public class Sucesso : Retorno
    {
        public Sucesso() : base(true) { }

        public Sucesso(object o) : base (o) { }

        public Sucesso(object o, string mensagem) : base (o, mensagem) { }

        //public Sucesso(object o, EMensagens retorno) : base (o, retorno) { }

    }
}
