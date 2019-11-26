using System;
using System.Collections.Generic;
using System.Text;
using aoponto.modelos;

namespace aoponto.dados
{
    public interface ILoteDao
    {
        Lote Obter(int id);
        List<Lote> ListarComprasUsuario(int usuarioId);
        List<Lote> ListarVendasUsuario(int usuarioId);
        List<LoteEtapa> ListarEtapasLote(int loteId);
        List<Lote> ListarOfertasDisponiveis(int usuarioId);
    }
}
