using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using aoponto.dados;
using aoponto.modelos;

namespace aoponto.servicos
{
    public class CidadeServico : BaseServico<Cidade>, ICidadeServico
    {
        ICidadeDao _cidadeDao;


        public CidadeServico(AppDbContext contexto, IIdentificacao identificacao, ICidadeDao cidadeDao) : base (contexto, identificacao)
        {
            _cidadeDao = cidadeDao;
        }

        public Retorno Listar(int estadoId, string nome = "")
        {
            try
            {
                return Sucesso(_cidadeDao.ObterLista(estadoId, nome));                                
            }
            catch (Exception ex)
            {
                return Erro(ex);
            }
            
        }
    }
}
