using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Linq;
using aoponto.modelos;

namespace aoponto.dados
{
    public class RacaDao : BaseDao, IRacaDao
    {
        public RacaDao(AppDbContext contexto) : base(contexto) {}

        public List<Raca> Listar(bool somenteAtivos)
        {
            var query = (IQueryable<Raca>)_contexto.Racas;

            if (somenteAtivos) {
                query = query.Where(r => r.Ativo == true);
            }

            query = query.OrderBy(x => x.Nome);

            return query.ToList();
        }

        public Raca Obter(int id)
        {
            return _contexto.Racas.Where(r => r.Id == id).ToList().FirstOrDefault();
        }
    }
}
