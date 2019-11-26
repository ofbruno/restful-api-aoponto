using System;
using System.Collections.Generic;
using System.Text;

namespace aoponto.servicos
{
    class Erro : Retorno
    {

        public Erro(string mensagem) : base (false, mensagem)
        {
            this.GravarLog(mensagem);
        }

        //public Erro(EMensagens retorno) : base (false, retorno)
        //{
        //    this.GravarLog(retorno.Descricao());
        //}

        public Erro(Exception ex) : base(ex)
        {
            this.GravarLog(ex);
        }

        private void GravarLog(Exception ex)
        {
            //TODO
        }

        private void GravarLog(string menssagem)
        {
            //TODO
        }

    }
}
