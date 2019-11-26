using System;
using System.Collections.Generic;
using System.Text;
using aoponto.modelos;

namespace aoponto.dados
{
    public interface INotificacaoDao
    {
        Notificacao Obter(int id);

        NotificacaoUsuario ObterDadosLeitura(int notificacaoId, int usuarioId);

        List<Notificacao> ListarPorUsuario(int usuarioId, int usuarioTipo, int quantidade, int limiteIdSuperior = 0, int limiteIdInferior = 0);
    }
}
