using System;
using System.Collections.Generic;
using aoponto.modelos;

namespace aoponto.servicos
{
    public interface IProdutorServico : IBaseServico<Produtor>
    {
        Retorno Todos();
        Retorno ListarCompradores();
    }
}
