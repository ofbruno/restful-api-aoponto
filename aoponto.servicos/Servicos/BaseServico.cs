using Microsoft.EntityFrameworkCore;
using aoponto.dados;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Text;
using aoponto.modelos;
using System.Linq.Expressions;
using System.Linq;
using aoponto.lib;

namespace aoponto.servicos
{
    public class BaseServico<T> : IBaseServico<T> where T : class
    {
        internal readonly AppDbContext _contexto;

        private IIdentificacao _identificacao;
        public IIdentificacao Identificacao { get { return _identificacao; } }
        //public IIdentificacao Identificacao { get; set; }

        public BaseServico(AppDbContext contexto, IIdentificacao identificacao) 
        {
            this._contexto = contexto;
            this._identificacao = identificacao;
        }


        //public void SetIdentificacao(IIdentificacao identificacao)
        //{
        //    this._identificacao = identificacao;
        //}


        #region Acesso a dados do tipo do serviço
                
        public IEnumerable<T> GetAll()
        {
            return this._contexto.Set<T>().ToList();
        }

        public T GetOne(int id)
        {
            return this._contexto.Set<T>().Where(x => x.GetPropertyInt("Id") == id).FirstOrDefault();
        }

        public T GetOne(int id, params string[] includes)
        {
            var query = from r in _contexto.Set<T>() select r;

            foreach (var include in includes)
            {
                query = query.Include(include);                
            }

            return query.Where(x => x.GetPropertyInt("Id") == id).FirstOrDefault();
        }

        public void Insert(T entidade)
        {
            this._contexto.Set<T>().Add(entidade);
        }

        public void Update(T entidade)
        {
            this._contexto.Set<T>().Update(entidade);
        }

        public void Delete(T entidade)
        {
            this._contexto.Set<T>().Remove(entidade);
        }

        public void Save()
        {
            this._contexto.SaveChanges();
        }

        #endregion
        
        #region Acesso a dados genérico

        internal IEnumerable<TIPO> GetAll<TIPO>() where TIPO : class
        {
            return _contexto.Set<TIPO>().ToList();
        }

        internal TIPO GetOne<TIPO>(int id) where TIPO : class
        {
            return this._contexto.Set<TIPO>().Where(x => x.GetPropertyInt("Id") == id).FirstOrDefault();
        }

        internal TIPO GetOne<TIPO>(int id, params string[] includes) where TIPO : class
        {
            var query = from r in _contexto.Set<TIPO>() select r;

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return query.Where(x => x.GetPropertyInt("Id") == id).FirstOrDefault();
        }

        internal void Insert<TIPO>(TIPO entidade) where TIPO : class
        {
            _contexto.Add<TIPO>(entidade);
        }

        internal void Update<TIPO>(TIPO entidade) where TIPO : class
        {
            _contexto.Update<TIPO>(entidade);            
        }

        internal IDbContextTransaction BeginTransaction()
        {
            return _contexto.Database.BeginTransaction();
        }

        #endregion

        #region Retornos

        #region Sucesso

        public Retorno Sucesso()
        {
            return new Sucesso();
        }

        public Retorno Sucesso(object o)
        {
            return new Sucesso(o);
        }

        public Retorno Sucesso(object o, string mensagem)
        {
            return new Sucesso(o, mensagem);
        }

        public Retorno Sucesso(List<object> lista)
        {
            return new Sucesso(lista);
        }

        public Retorno Sucesso(List<object> lista, string mensagem)
        {
            return new Sucesso(lista, mensagem);
        }

        #endregion

        #region Falha

        public Retorno Falha()
        {
            return new Falha();
        }

        public Retorno Falha(string mensagem)
        {
            return new Falha(mensagem);
        }

        public Retorno Falha(ETipoFalha tipo)
        {
            return new Falha(tipo);
        }

        #endregion

        #region Erro

        public Retorno Erro(string mensagem)
        {
            return new Erro(mensagem);
        }

        public Retorno Erro(Exception ex)
        {
            return new Erro(ex);
        }

        #endregion

        #endregion
    }
}
