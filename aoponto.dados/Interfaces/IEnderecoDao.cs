using System;
using System.Collections.Generic;
using System.Text;
using aoponto.modelos;

namespace aoponto.dados
{
    public interface IEnderecoDao
    {
        Endereco Obter(int id);
    }
}
