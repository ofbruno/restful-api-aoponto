using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Linq;
using aoponto.modelos;

namespace aoponto.dados
{
    public class CidadeDao : BaseDao, ICidadeDao
    {
        public CidadeDao(AppDbContext contexto) : base(contexto)
        {

        }

        public List<Cidade> ObterLista(int estadoId, string nome = "")
        {
            var query = _contexto.Cidades.Where(c => c.EstadoId == estadoId);

            if (!string.IsNullOrEmpty(nome)) {
                query = query.Where(c => c.Nome.Contains(nome));
            }

            query = query.OrderBy(x => x.Nome);

            return query.ToList();
        }
    }
}
