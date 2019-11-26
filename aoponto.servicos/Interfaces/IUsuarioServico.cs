using System;
using System.Collections.Generic;
using aoponto.modelos;

namespace aoponto.servicos
{
    public interface IUsuarioServico : IBaseServico<Usuario>
    {
        bool ExisteUsuario(int id = 0, string email = "", string firebaseId = "");
        Retorno Obter();
        Retorno ObterPorId(int id);
        Retorno ObterPorIdFirebase(string id);
        Retorno Inserir(Usuario usuario);
        Retorno Atualizar(ViewPerfil usuario);
        Retorno Excluir(int id);
        Retorno AtualizarAparelho(Aparelho aparelho);
    }
}
