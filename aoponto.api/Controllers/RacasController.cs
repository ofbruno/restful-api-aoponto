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
    public class RacasController : BaseController
    {
        private readonly IRacaServico _servico;


        public RacasController(IRacaServico servico)
        {
            _servico = servico;
        }

        [HttpGet]
        public IActionResult Listar()
        {
            try
            {
                return ResultadoComRetorno<List<Raca>>(_servico.Listar(true));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
