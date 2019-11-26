using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Linq;
using aoponto.modelos;

namespace aoponto.dados
{
    public class EnderecoDao : BaseDao, IEnderecoDao
    {
        public EnderecoDao(AppDbContext contexto) : base(contexto)
        {

        }

        public Endereco Obter(int id)
        {
            return _contexto.Enderecos.Where(x => x.Id == id).ToList().FirstOrDefault();
        }
    }
}
