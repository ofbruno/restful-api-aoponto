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
using Microsoft.AspNetCore.Http;

namespace aoponto.api.Controllers
{
    [Route("api/[controller]")]
    [ModelValidation]
    public class ProdutoresController : BaseController
    {
        private readonly IProdutorServico _servico;

       
        public ProdutoresController(IProdutorServico servico)
        {
            _servico = servico;
        }

        [HttpGet]
        public IActionResult Listar()
        {
            try
            {
                return ResultadoComRetorno<List<Produtor>>(_servico.Todos());
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
