using System;
using System.Collections.Generic;
using aoponto.modelos;

namespace aoponto.servicos
{
    public interface IFrigorificoServico : IBaseServico<Frigorifico>
    {
        Retorno Todos();
        Retorno Obter(int id);
        Retorno ObterPorUsuario();
        
    }
}
