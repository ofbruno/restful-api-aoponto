using System;
using System.Collections.Generic;
using aoponto.dados;
using aoponto.modelos;

namespace aoponto.servicos
{
    public interface INotificacaoServico : IBaseServico<Notificacao>
    {
        Retorno ObterListaPorUsuario(int quantidade = 0, int limiteIdSuperior = 0, int limiteIdInferior = 0);
        Retorno MarcarComoLido(int id);
        Retorno CriarNotificacaoNovaOfertaLoteParaFrigorificos(Lote lote);
        Retorno CriarNotificacaoNovaOfertaLoteParaProdutores(Lote lote);
    }
}
