using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace aoponto.modelos
{
    public enum EGeneroLote
    {
        [Description("Fêmeas")]
        Femeas = 1,

        [Description("Machos")]
        Machos = 2,

        [Description("Machos castrados")]
        MachosCastrados = 3
    }
}
