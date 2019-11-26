using aoponto.modelos;
using aoponto.servicos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace aoponto.api.Controllers
{
    [Route("api/[controller]")]
    [ModelValidation]
    public class OfertasLoteController : BaseController
    {
        private readonly IOfertaLoteServico _servico;

       
        public OfertasLoteController(IOfertaLoteServico servico)
        {
            _servico = servico;
        }



        [HttpGet]
        public IActionResult Index()
        {
            try
            {
                return ResultadoComRetorno<List<ViewOfertaConsulta>>(_servico.ListarOfertasDisponiveis());
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpGet("compras")]
        public IActionResult ListarCompras()
        {
            try
            {
                return ResultadoComRetorno<List<ViewLoteCompra>>(_servico.ListarComprasLote());
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpGet("vendas")]
        public IActionResult ListarVendas()
        {
            try
            {
                return ResultadoComRetorno<List<ViewLoteVenda>>(_servico.ListarVendasLote());
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        
        //[HttpGet("{loteId}")]
        //public IActionResult ObterPorId(int loteId)
        //{
        //    try
        //    {
        //        return ResultadoComRetorno<ViewLoteConsulta>(_servico.Obter(loteId));
        //    }
        //    catch (Exception ex)
        //    {
        //        return InternalServerError(ex);
        //    }
        //}

        [HttpGet("venda/detalhes/{loteId}")]
        public IActionResult ConsultarDetalhesVenda(int loteId)
        {
            try
            {
                return ResultadoComRetorno<ViewLoteDetalhes>(_servico.ConsultarDetalhesVenda(loteId));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpGet("compra/detalhes/{loteId}")]
        public IActionResult ConsultarDetalhesCompra(int loteId)
        {
            try
            {
                return ResultadoComRetorno<ViewLoteDetalhes>(_servico.ConsultarDetalhesCompra(loteId));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPost("selecionar")]
        public IActionResult SelecionarLote(int loteId)
        {
            try
            {
                return Resultado(_servico.SelecionarLoteOfertado(loteId));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPost]
        public IActionResult Criar([FromBody] ViewLote oferta)
        {
            try
            {
                return ResultadoComRetorno<Lote>(_servico.Inserir(oferta));                
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPut]
        public IActionResult Atualizar([FromBody] Lote lote)
        {
            try
            {
                return ResultadoComRetorno<ViewLote>(_servico.Atualizar(lote));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }        
    }
}
