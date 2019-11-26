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
    public class CidadesController : BaseController
    {
        private readonly ICidadeServico _servico;


        public CidadesController(ICidadeServico servico)
        {
            _servico = servico;
        }

        [HttpGet("{estadoId}")]
        public IActionResult Listar(int estadoId, string nome)
        {
            if (estadoId <= 0)
            {
                return BadRequest(EMensagensApi.DadosObrigatoriosNaoInformados);
            }
            
            try
            {
                return ResultadoComRetorno<List<Cidade>>(_servico.Listar(estadoId, nome));                
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
