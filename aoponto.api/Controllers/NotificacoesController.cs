using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using aoponto.modelos;
using aoponto.servicos;
using Microsoft.AspNetCore.Mvc;

namespace aoponto.api.Controllers
{
    [Route("api/[controller]")]
    [ModelValidation]
    public class NotificacoesController : BaseController
    {
        private readonly INotificacaoServico _servico;

        public NotificacoesController(INotificacaoServico servico)
        {
            _servico = servico;
        }

        [HttpGet("usuario")]
        public IActionResult ListarPorUsuario(int quantidade, int limiteIdSuperior, int limiteIdInferior)
        {
            try
            {
                return ResultadoComRetorno<List<ViewNotificacao>>(_servico.ObterListaPorUsuario(quantidade, limiteIdSuperior, limiteIdInferior));                
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPost("ler/{id}")]
        public IActionResult MarcarComoLido([FromRoute] int id)
        {
            if (id <= 0)
            {
                return BadRequest(EMensagensApi.DadosObrigatoriosNaoInformados);
            }

            try
            {
                return Resultado(_servico.MarcarComoLido(id));                
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }            
        }
    }
}