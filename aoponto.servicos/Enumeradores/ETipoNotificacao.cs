using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace aoponto.servicos
{
    public enum ETipoNotificacao
    {
        [Description("Oferta de lote para frigorificos")]
        NovaOfertaLoteParaFrigorificos = 1,

        [Description("Oferta de lote para produtores")]
        NovaOfertaLoteParaProdutores = 2
    }
}
