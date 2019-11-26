using System;
using System.Collections.Generic;
using System.Text;
using aoponto.modelos;

namespace aoponto.dados
{
    public interface IRacaDao
    {
        Raca Obter(int id);
        List<Raca> Listar(bool somenteAtivos);
    }
}
