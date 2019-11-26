using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace aoponto.modelos
{
    public enum ERaca
    {
        [Description("Angus")]
        Angus = 1,

        [Description("Red angus")]
        RedAngus = 2,

        [Description("Meio sangue angus")]
        MeioSangueAngus = 3
    }
}
