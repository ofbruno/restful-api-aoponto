using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using aoponto.dados;
using aoponto.servicos;
using aoponto.modelos;

namespace aoponto.api.Controllers
{
    [Route("api/[controller]")]
    [ModelValidation]
    public class HomeController : BaseController
    {
        private readonly IHomeServico _servico;


        public HomeController(IHomeServico servico)
        {
            _servico = servico;
        }

        [HttpGet]
        public IActionResult Index(int quantidade, int limiteIdSuperior, int limiteIdInferior)
        {            
            try
            {
                return ResultadoComRetorno<List<ViewFeed>>(_servico.ListarFeed(quantidade, limiteIdSuperior, limiteIdInferior));                
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
