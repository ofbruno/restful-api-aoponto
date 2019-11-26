using System;
using System.Collections.Generic;
using System.Text;
using aoponto.modelos;

namespace aoponto.dados
{
    public interface IUsuarioDao
    {
        Usuario Obter(int id, int token);

        List<Usuario> Listar(int id = 0, string email = "", string firebaseId = "");
    }
}
