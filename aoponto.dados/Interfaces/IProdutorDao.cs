using System;
using System.Collections.Generic;
using System.Text;
using aoponto.modelos;

namespace aoponto.dados
{
    public interface IProdutorDao
    {
        List<Produtor> Listar();
        Produtor Obter(int id);
        List<Produtor> ListarPorUsuario(int usuarioId);
        List<Produtor> ListarCompradores();
    }
}
