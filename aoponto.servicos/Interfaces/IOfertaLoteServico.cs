using System;
using System.Collections.Generic;
using aoponto.modelos;

namespace aoponto.servicos
{
    public interface IOfertaLoteServico : IBaseServico<Lote>
    {
        Retorno Obter(int id);
        Retorno Inserir(ViewLote oferta);
        Retorno Atualizar(Lote oferta);
        Retorno Cancelar(int id);
        Retorno SelecionarLoteOfertado(int loteId);
        Retorno ListarOfertasDisponiveis();
        Retorno ListarComprasLote();
        Retorno ListarVendasLote();
        Retorno ConsultarDetalhesVenda(int loteId);
        Retorno ConsultarDetalhesCompra(int loteId);
    }
}
