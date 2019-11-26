using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using aoponto.servicos;
using Microsoft.AspNetCore.Mvc;
using aoponto.modelos;

namespace aoponto.api.Controllers
{
    [Route("api/[controller]")]
    [ModelValidation]
    public class NoticiasController : BaseController
    {
        private readonly INoticiaServico _servico;

        public NoticiasController(INoticiaServico servico)
        {
            _servico = servico;
        }

        [HttpGet]
        public IActionResult Listar(int quantidade, int limiteIdSuperior, int limiteIdInferior)
        {
            try
            {
                return ResultadoComRetorno<List<ViewNoticia>>(_servico.Listar(quantidade, limiteIdSuperior, limiteIdInferior));                
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }            
        }

        [HttpGet("{id}")]
        public IActionResult Obter(int id)
        {
            try
            {
                return ResultadoComRetorno<ViewNoticia>(_servico.Obter(id));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}