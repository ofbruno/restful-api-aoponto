using System;
using System.Collections.Generic;
using aoponto.modelos;

namespace aoponto.servicos
{
    public interface IPushServico : IBaseServico<Push>
    {
        Retorno Enviar(Notificacao notificacao, bool enviarParaTodos);
    }
}
