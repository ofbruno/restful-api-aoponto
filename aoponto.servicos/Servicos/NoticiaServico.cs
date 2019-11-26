using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using aoponto.dados;
using aoponto.modelos;

namespace aoponto.servicos
{
    public class NoticiaServico : BaseServico<Noticia>, INoticiaServico
    {
        INoticiaDao _noticiaDao;

        public NoticiaServico(AppDbContext contexto, IIdentificacao identificacao, INoticiaDao noticiaDao) : base (contexto, identificacao)
        {
            _noticiaDao = noticiaDao;
        }

        public Retorno Obter(int id)
        {
            var noticia = this.GetOne(id);

            if (noticia == null)
            {
                return Falha(ETipoFalha.RegistroNaoEncontrado);
            }
            else
            {
                return Sucesso((ViewNoticia)noticia);
            }
        }

        public Retorno Listar(int quantidade = 0, int limiteIdSuperior = 0, int limiteIdInferior = 0)
        {
            if (quantidade == 0)
            {
                quantidade = 10;
            }

            try
            {
                var views = new List<ViewNoticia>();
                var noticias = _noticiaDao.Listar(quantidade, limiteIdSuperior, limiteIdInferior);

                noticias.ForEach(n => {
                    n.Conteudo = "";
                    views.Add((ViewNoticia)n);
                });

                return Sucesso(views);
            }
            catch (Exception ex)
            {
                return Erro(ex);
            }
        }
    }
}
