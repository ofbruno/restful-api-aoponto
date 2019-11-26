using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using aoponto.dados;
using aoponto.modelos;
using Microsoft.EntityFrameworkCore;

namespace aoponto.servicos
{
    public class LoginServico : BaseServico<Usuario>, ILoginServico
    {
        IUsuarioDao _usuarioDao;


        public LoginServico(AppDbContext contexto, IIdentificacao identificacao, IUsuarioDao usuarioDao) : base(contexto, identificacao)
        {
            _usuarioDao = usuarioDao;
        }

        public Retorno Login(ViewLogin login)
        {
            var usuario = null as Usuario;

            try
            {
                var usuarios = _usuarioDao.Listar(0, login.Email, login.FirebaseId);

                if (!usuarios.Any())
                {
                    usuario = new Usuario()
                    {
                        Email = login.Email,
                        FirebaseId = login.FirebaseId,
                        Nome = login.Nome,
                        Token = lib.Util.GerarTokenNumerico(),
                        Tipo = login.TipoUsuario,
                        DataHoraUltimoLogin = DateTime.Now,
                        DataHoraUltimoAcesso = DateTime.Now,
                        DataHoraCadastro = DateTime.Now,
                        Ativo = true,
                        FotoUrl = login.FotoUrl,
                        Telefone = login.Telefone
                    };

                    if (string.IsNullOrEmpty(usuario.Nome))
                    {
                        usuario.Nome = usuario.Email.Split('@')[0];
                        usuario.Nome = usuario.Nome.Replace('.', ' ').Replace('-', ' ');
                    }

                    _contexto.Add<Usuario>(usuario);

                    ETipoUsuario tipo = (ETipoUsuario)usuario.Tipo;

                    switch (tipo)
                    {
                        case ETipoUsuario.Frigorifico:
                            var f = new Frigorifico() { UsuarioId = usuario.Id };
                            _contexto.Add<Frigorifico>(f);
                            break;

                        case ETipoUsuario.Produtor:
                            var p = new Produtor() { UsuarioId = usuario.Id, Comprador = false };
                            _contexto.Add<Produtor>(p);
                            break;

                        case ETipoUsuario.Tecnico:
                            var t = new Tecnico() { UsuarioId = usuario.Id };
                            _contexto.Add<Tecnico>(t);
                            break;
                    }

                    _contexto.SaveChanges();
                }
                else
                {
                    usuario = usuarios.First();
                    usuario.Token = lib.Util.GerarTokenNumerico();
                    usuario.DataHoraUltimoLogin = DateTime.Now;

                    this.Save();
                }

                return Sucesso((ViewPerfil)usuario);
            }
            catch (Exception ex)
            {
                return Erro(ex);
            }
        }

        private void CriarUsuario(Usuario usuario)
        {
            
        }
    }
}
