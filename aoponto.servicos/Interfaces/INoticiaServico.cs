using System;
using System.Collections.Generic;
using aoponto.modelos;

namespace aoponto.servicos
{
    public interface INoticiaServico : IBaseServico<Noticia>
    {
        Retorno Obter(int id);

        Retorno Listar(int quantidade = 0, int limiteIdSuperior = 0, int limiteIdInferior = 0);
    }
}
