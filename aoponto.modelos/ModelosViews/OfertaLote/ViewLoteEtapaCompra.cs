using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace aoponto.modelos
{
    public class ViewLoteEtapaCompra
    {
        public int status { get; set; }
        public string statusDescricao { get; set; }
        public string statusDescricaoDetalhada { get; set; }
        public string usuario { get; set; }
        public string dataHora { get; set; }


        public static explicit operator ViewLoteEtapaCompra(LoteEtapa etapa)
        {
            var view = new ViewLoteEtapaCompra
            {
                status = etapa.Status,
                dataHora = etapa.DataHora.ToString("dd/MM/yyyy hh:mm"),
                statusDescricao = ((EStatusLote)etapa.Status).ToString(),
                
            };

            if (etapa.Usuario != null)
            {
                view.usuario = etapa.Usuario.Nome;
            }

            switch ((EStatusLote)etapa.Status)
            {
                case EStatusLote.Ofertado: view.statusDescricaoDetalhada = "Vendedor ofertou o lote"; break;
                case EStatusLote.SelecionadoParaCompra: view.statusDescricaoDetalhada = "Você selecionou o lote para compra"; break;
                case EStatusLote.PrecoAceitoPeloVendedor: view.statusDescricaoDetalhada = "O vendedor aceitou seu preço"; break;
                case EStatusLote.EncaminhadoParaAnaliseDoTecnico: view.statusDescricaoDetalhada = "Lote em análise pelo técnico"; break;
                case EStatusLote.LiberadoPeloTecnico: view.statusDescricaoDetalhada = "Lote liberado pelo técnico"; break;
                case EStatusLote.AguardandoTransporte: view.statusDescricaoDetalhada = "Aguardando transporte"; break;
                case EStatusLote.EmTransporte: view.statusDescricaoDetalhada = "Em transporte"; break;
                case EStatusLote.ChegouAoComprador: view.statusDescricaoDetalhada = "Seu lote chegou"; break;
            }
            
            return view;
        }        
    }
}
