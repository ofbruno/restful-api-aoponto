using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace aoponto.api
{
    public enum EMensagensApi
    {
        [Description("Dados obrigatórios não informados.")]
        DadosObrigatoriosNaoInformados,

        [Description("Registro não encontrado.")]
        RegistroNaoEncontrado,

        [Description("Ação não permitida para o usuário.")]
        NaoPermitido
    }
}
