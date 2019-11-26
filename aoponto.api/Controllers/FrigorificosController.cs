using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using aoponto.servicos;
using Microsoft.AspNetCore.Mvc;

namespace aoponto.api.Controllers
{
    [Route("api/[controller]")]
    [ModelValidation]
    public class FrigorificosController : BaseController
    {
        private readonly IFrigorificoServico _servico;

        public FrigorificosController(IFrigorificoServico servico)
        {
            _servico = servico;
        }

        [HttpGet("lotes")]
        public IActionResult ListarLotes()
        {
            return Ok();            
        }

        [HttpGet("lotes/{id}")]
        public IActionResult ObterLote(int id)
        {
            return Ok();
        }

        
    }
}