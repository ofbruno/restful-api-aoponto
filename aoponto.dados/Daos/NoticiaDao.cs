using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Linq;
using aoponto.modelos;

namespace aoponto.dados
{
    public class NoticiaDao : BaseDao, INoticiaDao
    {
        public NoticiaDao(AppDbContext contexto) : base(contexto)
        {

        }

        public List<Noticia> Listar(int quantidade, int limiteIdSuperior = 0, int limiteIdInferior = 0)
        {
            var query = from n in _contexto.Noticias select n;

            query = query.OrderByDescending(n => n.DataHora);

            if (limiteIdSuperior > 0)
            {
                query = query.Where(n => n.Id < limiteIdSuperior);
            }

            if (limiteIdInferior > 0)
            {
                query = query.Where(n => n.Id > limiteIdInferior);
            }

            if ((limiteIdInferior == 0 && limiteIdSuperior == 0) || (limiteIdSuperior > 0 && limiteIdInferior == 0))
            {
                query = query.Take(quantidade);
            }


            return query.ToList();
        }
    }
}
