using System;
using System.Collections.Generic;
using aoponto.modelos;

namespace aoponto.servicos
{
    public interface ICidadeServico : IBaseServico<Cidade>
    {
        Retorno Listar(int estadoId, string nome = "");
    }
}
