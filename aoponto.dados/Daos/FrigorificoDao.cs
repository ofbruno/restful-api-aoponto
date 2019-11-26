using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Linq;
using aoponto.modelos;

namespace aoponto.dados
{
    public class FrigorificoDao : BaseDao, IFrigorificoDao
    {
        public FrigorificoDao(AppDbContext contexto) : base(contexto) {}


        public List<Frigorifico> Listar()
        {
            return _contexto.Frigorificos.Include("Usuario").ToList();
        }

        public Frigorifico Obter(int id)
        {
            var frigorificos = _contexto.Frigorificos.Include("Usuario").Where(f => f.Id == id).ToList();
            return frigorificos.FirstOrDefault();
        }

        public List<Frigorifico> ListarPorUsuario(int usuarioId)
        {
            var frigorificos = _contexto.Frigorificos.Include("Usuario").Where(f => f.UsuarioId == usuarioId).ToList();
            return frigorificos;
        }
    }
}
