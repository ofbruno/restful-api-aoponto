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
    public class LoginController : BaseController
    {
        private readonly ILoginServico _servico;

       
        public LoginController(ILoginServico servico)
        {
            _servico = servico;
        }
        

        [HttpPost]        
        public IActionResult Login([FromBody] ViewLogin login)
        {
            if (login == null)
            {
                return BadRequest(EMensagensApi.DadosObrigatoriosNaoInformados);
            }

            try
            {
                return ResultadoComRetorno<ViewPerfil>(_servico.Login(login));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }            
        }

    }
}
