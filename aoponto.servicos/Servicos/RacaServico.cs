using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using aoponto.dados;
using aoponto.modelos;

namespace aoponto.servicos
{
    public class RacaServico : BaseServico<Raca>, IRacaServico
    {
        IRacaDao _racaDao;

        public RacaServico(AppDbContext contexto, IIdentificacao identificacao, IRacaDao racaDao) : base (contexto, identificacao)
        {
            _racaDao = racaDao;
        }
        
        public Retorno Obter(int id)
        {
            var raca = _racaDao.Obter(id);

            if (raca == null)
            {
                return Falha(ETipoFalha.RegistroNaoEncontrado);
            }
            else
            {
                return Sucesso(raca);
            }
        }

        public Retorno Listar(bool somenteAtivos)
        {
            try
            {
                return Sucesso(_racaDao.Listar(somenteAtivos));
            }
            catch (Exception ex)
            {
                return Erro(ex);
            }
        }
    }
}
