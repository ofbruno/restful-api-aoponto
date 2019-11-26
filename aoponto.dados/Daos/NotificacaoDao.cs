using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Linq;
using aoponto.modelos;

namespace aoponto.dados
{
    public class NotificacaoDao : BaseDao, INotificacaoDao
    {
        public NotificacaoDao(AppDbContext contexto) : base(contexto)
        {

        }

        public Notificacao Obter(int id)
        {
            return _contexto.Notificacoes.FirstOrDefault(n => n.Id == id);
        }

        public NotificacaoUsuario ObterDadosLeitura(int notificacaoId, int usuarioId)
        {
            return _contexto.NotificacoesUsuarios.Where(x => x.NotificacaoId == notificacaoId && x.UsuarioId == usuarioId).ToList().FirstOrDefault();
        }

        public List<Notificacao> ListarPorUsuario(int usuarioId, int usuarioTipo, int quantidade, int limiteIdSuperior = 0, int limiteIdInferior = 0)
        {
            var select = @"
                SELECT n.Id, n.Tipo, n.Titulo, n.Mensagem, n.DataHora, n.Parametros, n.EnviaPush, n.TipoDestino, n.TipoUsuarioDestino, nu.DataHoraLeitura
                FROM aoponto.Notificacoes n inner join aoponto.NotificacoesUsuarios nu on n.Id = nu.NotificacaoId
                WHERE nu.UsuarioId = @usuarioId
                UNION
                SELECT n.Id, n.Tipo, n.Titulo, n.Mensagem, n.DataHora, n.Parametros, n.EnviaPush, n.TipoDestino, n.TipoUsuarioDestino, STR_TO_DATE('1,1,1900','%d,%m,%Y') as DataHoraLeitura
                FROM aoponto.Notificacoes n
                WHERE (n.TipoDestino = 2 OR (n.TipoDestino = 3 AND n.TipoUsuarioDestino = @usuarioTipo))
                AND NOT EXISTS(SELECT 1 FROM aoponto.NotificacoesUsuarios nu WHERE nu.NotificacaoId = n.Id AND nu.UsuarioId = @usuarioId) ";

            select = select.Replace("@usuarioId", usuarioId.ToString());
            select = select.Replace("@usuarioTipo", usuarioTipo.ToString());

            var query = _contexto.Notificacoes.FromSql(select);

            query = query.OrderByDescending(n => n.DataHora);

            if (limiteIdSuperior > 0)
            {
                query = query.Where(n => n.Id < limiteIdSuperior);
            }

            if (limiteIdInferior > 0)
            {
                query = query.Where(n => n.Id > limiteIdInferior);
            }

            if ((limiteIdInferior == 0 && limiteIdSuperior == 0) || (limiteIdSuperior > 0 && limiteIdInferior == 0))
            {
                query = query.Take(quantidade);
            }


            return query.ToList();
        }
        
    }
}
