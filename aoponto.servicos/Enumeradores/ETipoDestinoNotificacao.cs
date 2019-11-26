using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace aoponto.servicos
{
    public enum ETipoDestinoNotificacao
    {
        [Description("Push destinado à um usuário específico")]
        Individual = 1,

        [Description("Push destinado à todos os usuários do sistema")]
        TodosUsuarios = 2,

        [Description("Push destinado à todos os usuários de um determinado tipo")]
        PorTipoUsuario = 3
    }
}
