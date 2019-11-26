using System;
using System.Collections.Generic;
using System.Text;
using aoponto.modelos;

namespace aoponto.dados
{
    public interface ICidadeDao
    {
        List<Cidade> ObterLista(int estadoId, string nome = "");
    }
}
