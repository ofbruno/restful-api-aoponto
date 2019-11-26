using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace aoponto.modelos
{
    public enum ETipoLote
    {
        [Description("Frigoríficos")]
        Frigorificos = 1,

        [Description("Produtores")]
        OutrosProdutores = 2
    }
}
