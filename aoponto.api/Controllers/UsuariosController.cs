using aoponto.modelos;
using aoponto.servicos;
using Microsoft.AspNetCore.Mvc;
using System;

namespace aoponto.api.Controllers
{
    [Route("api/[controller]")]
    [ModelValidation]
    public class UsuariosController : BaseController
    {
        private readonly IUsuarioServico _servico;

       
        public UsuariosController(IUsuarioServico servico)
        {
            _servico = servico;
        }
        
        [HttpGet]
        public IActionResult Obter()
        {
            try
            {
                var retorno = _servico.Obter();

                if (retorno.Sucesso)
                {
                    return Ok(retorno.ObterDados<Usuario>());
                }
                else if(retorno.TipoFalha == ETipoFalha.RegistroNaoEncontrado)
                {
                    return NotFound();
                }
                else
                {
                    return InternalServerError();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpGet("firebase/{id}")]
        public IActionResult ObterPorIdFirebase(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest(EMensagensApi.DadosObrigatoriosNaoInformados);
            }

            try
            {
                return ResultadoComRetorno<Usuario>(_servico.ObterPorIdFirebase(id));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }            
        }

        [HttpPost]
        public IActionResult Criar([FromBody] Usuario usuario)
        {
            if (usuario == null)
            {
                return BadRequest(EMensagensApi.DadosObrigatoriosNaoInformados);
            }

            try
            {
                return ResultadoComRetorno<Usuario>(_servico.Inserir(usuario));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }            
        }

        [HttpPut]
        public IActionResult Atualizar([FromBody] ViewPerfil usuario)
        {
            if (usuario == null)
            {
                return BadRequest(EMensagensApi.DadosObrigatoriosNaoInformados);
            }

            try
            {
                return Resultado(_servico.Atualizar(usuario));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPost("aparelho")]
        public IActionResult AtualizarAparelho([FromBody] Aparelho aparelho)
        {
            try
            {
                return Resultado(_servico.AtualizarAparelho(aparelho));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
