using System;
using System.Collections.Generic;
using System.Text;

namespace aoponto.servicos
{
    class Falha : Retorno
    {
        public Falha() : base (false) { }

        public Falha(string mensagem) : base (false, mensagem) { }

        //public Falha(EMensagens retorno) : base (false, retorno) { }

        public Falha(ETipoFalha tipo) : base(tipo) { }

    }
}
