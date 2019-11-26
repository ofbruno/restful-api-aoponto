using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace aoponto.servicos
{
    public enum ETipoFalha
    {
        SemFalhas,
        NaoDefinido,
        Excessao,
        RegistroNaoEncontrado,
        DadosObrigatoriosNaoInformados,
        NaoPermitido
    }
}
