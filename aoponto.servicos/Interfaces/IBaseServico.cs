using aoponto.modelos;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace aoponto.servicos
{
    public interface IBaseServico<T>
    {
        IIdentificacao Identificacao { get; }
        //IIdentificacao Identificacao { get; set; }

        //void SetIdentificacao(IIdentificacao identificacao);
        IEnumerable<T> GetAll();
        T GetOne(int id);
        T GetOne(int id, params string[] includes);
        //IEnumerable<T> Listar(Expression<Func<T, bool>> expression);
        void Insert(T entity);
        void Update(T entity);
        void Delete(T entity);
        void Save();
    }
}
