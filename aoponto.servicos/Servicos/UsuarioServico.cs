using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using aoponto.dados;
using aoponto.modelos;
using Microsoft.EntityFrameworkCore;

namespace aoponto.servicos
{
    public class UsuarioServico : BaseServico<Usuario>, IUsuarioServico
    {
        IUsuarioDao _usuarioDao;


        public UsuarioServico(AppDbContext contexto, IIdentificacao identificacao, IUsuarioDao usuarioDao) : base(contexto, identificacao)
        {
            _usuarioDao = usuarioDao;
        }

        public bool ExisteUsuario(int id = 0, string email = "", string firebaseId = "")
        {
            var usuarios = _usuarioDao.Listar(id, email, firebaseId);
            return usuarios.Any();
        }

        public Retorno Obter()
        {
            var usuarios = _usuarioDao.Listar(this.Identificacao.Id);

            if (!usuarios.Any())
            {
                return Falha(ETipoFalha.RegistroNaoEncontrado);
            }
            else
            {
                return Sucesso(usuarios.First());
            }
        }

        public Retorno ObterPorId(int id)
        {
            var usuarios = _usuarioDao.Listar(id);

            if (!usuarios.Any())
            {
                return Falha(ETipoFalha.RegistroNaoEncontrado);
            }
            else
            {
                return Sucesso(usuarios.First());
            }
        }

        public Retorno ObterPorIdFirebase(string firebaseId)
        {
            var usuarios = _usuarioDao.Listar(0, "", firebaseId);

            if (!usuarios.Any())
            {
                return Falha(ETipoFalha.RegistroNaoEncontrado);
            }
            else
            {
                return Sucesso(usuarios.First());
            }
        }

        public Retorno Atualizar(ViewPerfil perfil)
        {
            try
            {
                if (perfil == null)
                {
                    return Falha(ETipoFalha.DadosObrigatoriosNaoInformados);
                }

                var retorno = this.ObterPorId(perfil.Id);

                if (!retorno.Sucesso)
                {
                    return retorno;
                }

                var usuario = retorno.ObterDados<Usuario>();

                usuario.Email = perfil.Email;                
                usuario.Nome = perfil.Nome;
                usuario.Telefone = perfil.Telefone;
                usuario.FotoUrl = perfil.FotoUrl;
                usuario.Ativo = true;

                if (perfil.Tipo == (int)ETipoUsuario.Produtor)
                {
                    var produtor = _contexto.Produtores.FirstOrDefault(x => x.UsuarioId == usuario.Id);

                    if (produtor != null)
                    {
                        produtor.Comprador = perfil.Comprador;
                        produtor.Usuario = null;

                        this.Update<Produtor>(produtor);
                    }
                }

                this.Update(usuario);
                this.Save();
                
                return Sucesso("Perfil atualizado com sucesso.");
            }
            catch (Exception ex)
            {
                return Erro(ex);
            }
        }

        public Retorno Excluir(int id)
        {
            return Falha("Não disponível");
        }

        public Retorno Inserir(Usuario usuario)
        {
            try
            {
                if (usuario == null)
                {
                    return Falha(ETipoFalha.DadosObrigatoriosNaoInformados);
                }

                if (this.ExisteUsuario(0, usuario.Email))
                {
                    return Falha("Este e-mail já está cadastrado.");
                }

                if (string.IsNullOrEmpty(usuario.Nome))
                {
                    usuario.Nome = usuario.Email.Split('@')[0];
                }

                usuario.Ativo = true;
                usuario.DataHoraCadastro = DateTime.Now;
                                
                _contexto.Usuarios.Add(usuario);
                _contexto.SaveChanges();

                return Sucesso(usuario, "Usuário criado com sucesso.");
            }
            catch (Exception ex)
            {
                return Erro(ex);
            }
        }

        public Retorno AtualizarAparelho(Aparelho aparelho)
        {
            try
            {
                var a = _contexto.Aparelhos.Where(x => x.Uuid == aparelho.Uuid).FirstOrDefault();

                if (a == null)
                {
                    aparelho.UsuarioId = this.Identificacao.Id;

                    _contexto.Aparelhos.Add(aparelho);
                }
                else
                {
                    a.UsuarioId = this.Identificacao.Id;
                    a.Uuid = aparelho.Uuid;
                    a.TokenPush = aparelho.TokenPush;
                    a.Modelo = aparelho.Modelo;

                    _contexto.Aparelhos.Update(a);
                }

                _contexto.SaveChanges();

                return Sucesso("Aparelho atualizado com sucesso.");
            }
            catch (Exception ex)
            {
                return Erro(ex);
            }
        }
    }
}
