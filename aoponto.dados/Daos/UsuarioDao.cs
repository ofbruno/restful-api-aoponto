using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Linq;
using aoponto.modelos;

namespace aoponto.dados
{
    public class UsuarioDao : BaseDao, IUsuarioDao
    {
        public UsuarioDao(AppDbContext contexto) : base(contexto) {}


        public Usuario Obter(int id, int token)
        {
            return _contexto.Usuarios.Where(u => u.Id == id && u.Token == token).ToList().FirstOrDefault();
        }

        public List<Usuario> Listar(int id = 0, string email = "", string firebaseId = "")
        {
            if (id == 0 && string.IsNullOrEmpty(email) && string.IsNullOrEmpty(firebaseId))
            {
                throw new Exception("Nenhum filtro informado para a busca.");
            }

            var query = (from u in _contexto.Usuarios select u);

            if (id > 0)
            {
                query = query.Where(u => u.Id == id);
            }

            if (!string.IsNullOrEmpty(email))
            {
                query = query.Where(u => u.Email == email.Trim());
            }

            if (!string.IsNullOrEmpty(firebaseId))
            {
                query = query.Where(u => u.FirebaseId == firebaseId.Trim());
            }

            return query.ToList();
        }

        
    }
}
