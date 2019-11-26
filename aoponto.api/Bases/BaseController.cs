using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using aoponto.modelos;
using aoponto.servicos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using aoponto.lib;

namespace aoponto.api.Controllers
{
    public class BaseController : Controller
    {
        private List<Object> _servicos = new List<object>();

        public IIdentificacao Identificacao { get { return this.HttpContext.Items["_identificacao"] as IIdentificacao; } }


        public override void OnActionExecuting(ActionExecutingContext context)
        {
            //_servicos.ForEach(x => x.GetType().GetProperty("Identificacao").SetValue(x, this.Identificacao));
            //_servicos.ForEach(x => x.GetType().GetMethod("SetIdentificacao").Invoke(x, new[] { this.Identificacao }));            
        }


        private void GravarLog(string mensagem)
        {
            //TODO: gravar log
        }

        private void GravarLog(Exception ex)
        {
            //TODO: gravar log
        }

        internal T RegistrarServico<T>(T referencia)
        {
            _servicos.Add(referencia);
            return referencia;
        }
                        
        internal OkObjectResult Ok(EMensagensApi mensagem)
        {
            return Ok(mensagem.Descricao());
        }

        internal BadRequestObjectResult BadRequest(EMensagensApi mensagem)
        {
            return BadRequest(mensagem.Descricao());
        }

        internal ObjectResult InternalServerError(string mensagem = "")
        {
            if (string.IsNullOrEmpty(mensagem))
            {               
                return StatusCode(500, "Infelizmente tivemos um problema no processamento desta solicitação. Tente novamente mais tarde.");
            }
            else
            {
                return StatusCode(500, mensagem);
            }
        }

        internal ObjectResult InternalServerError(Exception ex)
        {
            this.GravarLog(ex);
            return StatusCode(500, "Infelizmente tivemos um problema no processamento desta solicitação. Tente novamente mais tarde.");
        }

        internal IActionResult ResultadoComRetorno<T>(Retorno retorno)
        {
            if (retorno.Sucesso)
            {
                return Ok(retorno.ObterDados<T>());
            }
            else
            {
                if(retorno.TipoFalha == ETipoFalha.DadosObrigatoriosNaoInformados)
                {
                    return BadRequest(EMensagensApi.DadosObrigatoriosNaoInformados.Descricao());
                }
                else if (retorno.TipoFalha == ETipoFalha.RegistroNaoEncontrado)
                {
                    return NotFound(EMensagensApi.RegistroNaoEncontrado.Descricao());
                }
                else
                {
                    return InternalServerError(retorno.Mensagem);
                }
            }
        }

        internal IActionResult Resultado(Retorno retorno)
        {
            if (retorno.Sucesso)
            {
                return Ok();
            }
            else
            {
                if (retorno.TipoFalha == ETipoFalha.DadosObrigatoriosNaoInformados)
                {
                    return BadRequest(EMensagensApi.DadosObrigatoriosNaoInformados.Descricao());
                }
                else if (retorno.TipoFalha == ETipoFalha.RegistroNaoEncontrado)
                {
                    return NotFound(EMensagensApi.RegistroNaoEncontrado.Descricao());
                }
                else if (retorno.TipoFalha == ETipoFalha.NaoPermitido)
                {
                    return StatusCode(403, string.IsNullOrEmpty(retorno.Mensagem) ? EMensagensApi.NaoPermitido.Descricao() : retorno.Mensagem);
                }
                else
                {                    
                    return InternalServerError(retorno.Mensagem);
                }
            }
        }
    }
}
