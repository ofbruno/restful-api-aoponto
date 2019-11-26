using System;
using System.Collections.Generic;
using aoponto.modelos;

namespace aoponto.servicos
{
    public interface IHomeServico : IBaseServico<Usuario>
    {
        Retorno ListarFeed(int quantidade, int limiteIdSuperior, int limiteIdInferior);
    }
}
