using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using aoponto.dados;
using aoponto.modelos;
using Microsoft.EntityFrameworkCore;

namespace aoponto.servicos
{
    public class ProdutorServico : BaseServico<Produtor>, IProdutorServico
    {
        IProdutorDao _produtorDao;

        public ProdutorServico(AppDbContext contexto, IIdentificacao identificacao, IProdutorDao produtorDao) : base (contexto, identificacao)
        {
            _produtorDao = produtorDao;
        }

        public Retorno Todos()
        {
            try
            {
                return Sucesso(_produtorDao.Listar());
            }
            catch (Exception ex)
            {
                return Erro(ex);
            }
        }

        public Retorno ListarCompradores()
        {
            try
            {
                return Sucesso(_produtorDao.ListarCompradores());
            }
            catch (Exception ex)
            {
                return Erro(ex);
            }
        }
    }
}
