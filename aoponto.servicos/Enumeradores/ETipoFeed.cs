using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace aoponto.servicos
{
    public enum ETipoFeed
    {
        [Description("Notícia")]
        Noticia = 1,

        [Description("Notificação")]
        Notificacao = 2
    }
}
