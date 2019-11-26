using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace aoponto.modelos
{
    public enum ETipoFreteLote
    {
        [Description("Frete por conta do comprador")]
        Comprador = 1,

        [Description("Frete por conta do vendedor")]
        Vendedor = 2,

        [Description("Frete por conta do vendedor até {0} km")]
        VendedorLimiteDistancia = 3
    }
}
