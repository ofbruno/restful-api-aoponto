using System;
using System.Collections.Generic;
using System.Text;
using aoponto.modelos;

namespace aoponto.dados
{
    public interface INoticiaDao
    {
        List<Noticia> Listar(int quantidade, int limiteIdSuperior = 0, int limiteIdInferior = 0);
    }
}
