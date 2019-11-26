using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace aoponto.modelos
{
    public enum ETipoUsuario
    {
        [Description("Indefinido")]
        Indefinido = 0,

        [Description("Equipe Ao Ponto")]
        Proprietario = 1,

        [Description("Produtor")]
        Produtor = 2,

        [Description("Frigorífico")]
        Frigorifico = 3,

        [Description("Técnico")]
        Tecnico = 4,

        [Description("Cliente")]
        Cliente = 5
    }
}
