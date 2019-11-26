using System;
using System.Collections.Generic;
using System.Text;
using aoponto.modelos;

namespace aoponto.dados
{
    public interface IFrigorificoDao
    {
        List<Frigorifico> Listar();
        Frigorifico Obter(int id);
        List<Frigorifico> ListarPorUsuario(int usuarioId);
    }
}
