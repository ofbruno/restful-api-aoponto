using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Linq;
using aoponto.modelos;

namespace aoponto.dados
{
    public class LoteDao : BaseDao, ILoteDao
    {
        public LoteDao(AppDbContext contexto) : base(contexto)
        {

        }

        public Lote Obter(int id)
        {
            var lotes = (from l in _contexto.Lotes.Include("Raca").Include("Endereco").Include("Endereco.Estado").Include("Endereco.Cidade").Include("Etapas").Include("Anexos")
                         where l.Id == id
                         select l);

            return lotes.ToList().FirstOrDefault();
        }

        public List<Lote> ListarComprasUsuario(int usuarioId)
        {
            var query = (from l in _contexto.Lotes.Include("Raca").Include("Etapas").Include("Endereco").Include("Endereco.Estado").Include("Endereco.Cidade")
                         where l.UsuarioIdComprador == usuarioId
                         select l);

            return query.ToList();
        }

        public List<Lote> ListarVendasUsuario(int usuarioId)
        {
            var query = (from l in _contexto.Lotes.Include("Raca").Include("Etapas").Include("Endereco").Include("Endereco.Estado").Include("Endereco.Cidade")
                         where l.UsuarioIdVendedor == usuarioId
                         select l);

            return query.ToList();
        }

        public List<LoteEtapa> ListarEtapasLote(int loteId)
        {
            var query = (from l in _contexto.LoteEtapas
                         where l.LoteId == loteId
                         select l);

            return query.ToList();
        }

        public List<Lote> ListarOfertasDisponiveis(int usuarioId)
        {
            var query = (from l in _contexto.Lotes.Include("Raca").Include("Etapas").Include("Endereco").Include("Endereco.Estado").Include("Endereco.Cidade")
                         where l.UsuarioIdVendedor != usuarioId
                         select l);

            query = query.Where(l => l.Etapas.Count == 1);

            return query.ToList();
        }
    }
}
