using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using aoponto.dados;
using aoponto.modelos;
using Microsoft.EntityFrameworkCore;

namespace aoponto.servicos
{
    public class FrigorificoServico : BaseServico<Frigorifico>, IFrigorificoServico
    {
        IFrigorificoDao _frigorificoDao;

        public FrigorificoServico(AppDbContext contexto, IIdentificacao identificacao, IFrigorificoDao frigorificoDao) : base(contexto, identificacao)
        {
            _frigorificoDao = frigorificoDao;
        }
        
        public Retorno Obter(int id)
        {
            var frigorifico = _frigorificoDao.Obter(id);

            if (frigorifico == null)
            {
                return Falha(ETipoFalha.RegistroNaoEncontrado);
            }
            else
            {
                return Sucesso(frigorifico);
            }
        }

        public Retorno Todos()
        {
            try
            {
                return Sucesso(_frigorificoDao.Listar());
            }
            catch (Exception ex)
            {
                return Erro(ex);
            }
        }

        public Retorno ObterPorUsuario()
        {
            try
            {
                var frigorificos = _frigorificoDao.ListarPorUsuario(this.Identificacao.Id);             
                
                if (frigorificos == null || !frigorificos.Any())
                {
                    return Falha(ETipoFalha.RegistroNaoEncontrado);
                }
                else
                {
                    return Sucesso(frigorificos);
                }

            }
            catch (Exception ex)
            {
                return Erro(ex);
            }
        }        

        
    }
}
