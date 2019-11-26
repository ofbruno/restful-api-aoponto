using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Linq;
using aoponto.modelos;

namespace aoponto.dados
{
    public class ProdutorDao : BaseDao, IProdutorDao
    {
        public ProdutorDao(AppDbContext contexto) : base(contexto) {}


        public List<Produtor> Listar()
        {
            return _contexto.Produtores.Include("Usuario").ToList();
        }

        public List<Produtor> ListarCompradores()
        {
            return _contexto.Produtores.Include("Usuario").Where( x => x.Comprador == true).ToList();
        }

        public Produtor Obter(int id)
        {
            var produtores = _contexto.Produtores.Include("Usuario").Where(p => p.Id == id).ToList();
            return produtores.FirstOrDefault();
        }

        public List<Produtor> ListarPorUsuario(int usuarioId)
        {
            var produtores = _contexto.Produtores.Include("Usuario").Where(p => p.UsuarioId == usuarioId).ToList();
            return produtores;
        }
    }
}
