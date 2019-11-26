using System;
using System.Collections.Generic;
using aoponto.modelos;

namespace aoponto.servicos
{
    public interface IRacaServico : IBaseServico<Raca>
    {
        Retorno Obter(int id);
        Retorno Listar(bool somenteAtivos);
    }
}
