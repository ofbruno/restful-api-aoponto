using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Linq;
using aoponto.modelos;

namespace aoponto.dados
{
    public class PushDao : BaseDao, IPushDao
    {
        public PushDao(AppDbContext contexto) : base(contexto)
        {

        }

        public List<string> ObterListaTokenPushPorNotificacao(int notificacaoId)
        {
            var lista = new List<string>();

            var select = @"select ap.Id, ap.UsuarioId, ap.Uuid, ap.Modelo, ap.TokenPush 
                           from aoponto.Aparelhos ap inner join aoponto.NotificacoesUsuarios nu on ap.UsuarioId = nu.UsuarioId
                           where nu.NotificacaoId = @notificacaoId";

            select = select.Replace("@notificacaoId", notificacaoId.ToString());

            var aparelhos = _contexto.Aparelhos.FromSql(select).ToList();

            foreach (var a in aparelhos)
            {
                lista.Add(a.TokenPush);
            }

            return lista;
        }
    }
}
