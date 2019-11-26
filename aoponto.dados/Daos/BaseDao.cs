using System;
using System.Collections.Generic;
using System.Text;

namespace aoponto.dados
{
    public class BaseDao
    {
        internal readonly AppDbContext _contexto;

        public BaseDao(AppDbContext contexto)
        {
            this._contexto = contexto;
        }
    }
}
