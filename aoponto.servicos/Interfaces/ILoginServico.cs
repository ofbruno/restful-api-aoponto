using System;
using System.Collections.Generic;
using aoponto.modelos;

namespace aoponto.servicos
{
    public interface ILoginServico : IBaseServico<Usuario>
    {
        Retorno Login(ViewLogin login);
    }
}
