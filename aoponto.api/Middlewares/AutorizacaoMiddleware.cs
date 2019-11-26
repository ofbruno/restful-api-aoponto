using aoponto.dados;
using aoponto.servicos;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using aoponto.modelos;

namespace aoponto.api
{
    public class AutorizacaoMiddleware
    {
        private const int CODIGO_NAO_AUTORIZADO = 401;
        private const int CODIGO_ERRO_INTERNO = 500;

        private readonly RequestDelegate _next;
        private AppDbContext _dbcontexto;
        private IIdentificacao _identificacao;

        public AutorizacaoMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, AppDbContext dbcontexto, IIdentificacao identificacao)
        {
            _dbcontexto = dbcontexto;
            _identificacao = identificacao;
           

            if (!context.Request.Path.ToString().StartsWith("/api/login") && !context.Request.Path.ToString().StartsWith("/api/home"))
            {                
                try
                {
                    var header = context.Request.Headers.FirstOrDefault(x => x.Key == "x-token");
                    int token = 0;
                    int id = 0;

                    if (string.IsNullOrEmpty(header.Value))
                    {
                        context.Response.StatusCode = CODIGO_NAO_AUTORIZADO;
                        await context.Response.WriteAsync("Credenciais nao informadas.");
                        return;
                    }
                    else
                    {
                        var p = header.Value.ToString().Split(".");

                        if (p.Length == 2 && int.TryParse(p[0], out token) && int.TryParse(p[1], out id))
                        {
                            if (!this.CarregarUsuario(token, id))
                            {
                                context.Response.StatusCode = CODIGO_NAO_AUTORIZADO;
                                await context.Response.WriteAsync("Credenciais invalidas.");
                                return;
                            }                            
                        }
                    }

                    if (id == 0 || token == 0)
                    {
                        context.Response.StatusCode = CODIGO_NAO_AUTORIZADO;
                        await context.Response.WriteAsync("Credenciais invalidas.");
                        return;
                    }
                }
                catch (Exception ex)
                {
                    context.Response.StatusCode = CODIGO_ERRO_INTERNO;
                    await context.Response.WriteAsync("Falha na autorizacao: " + ex.Message);
                    return;
                }

                context.Items.Add("_identificacao", _identificacao);

                context.Response.OnStarting(state => {
                    var httpContext = (HttpContext)state;
                    httpContext.Response.Headers.Add("x-token", string.Format("{0}.{1}", _identificacao.Token, _identificacao.Id));
                    return Task.FromResult(0);
                }, context);
            }


            await _next(context);

        }

        private bool CarregarUsuario(int token, int id)
        {
            var usuario = _dbcontexto.Usuarios.Where(u => u.Id == id && u.Token == token).ToList().FirstOrDefault();

            if (usuario == null)
            {
                return false;
            }

            this.SalvarToken(usuario);

            return true;
        }

        private void SalvarToken(Usuario usuario)
        {
            usuario.Token = lib.Util.GerarTokenNumerico();

            using (var contexto = AppDbContextFactory.CreateDbContext())
            {
                contexto.Usuarios.Update(usuario);
                contexto.SaveChanges();
            }

            //_dbcontexto.Usuarios.Update(usuario);
            //_dbcontexto.SaveChanges();

            _identificacao.Id = usuario.Id;
            _identificacao.Tipo = usuario.Tipo;
            _identificacao.Token = usuario.Token;
            _identificacao.Nome = usuario.Nome;
            _identificacao.Email = usuario.Email;
        }        
    }
}
