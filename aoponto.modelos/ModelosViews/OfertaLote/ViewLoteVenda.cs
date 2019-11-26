using aoponto.lib;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace aoponto.modelos
{
    public class ViewLoteVenda
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Raca { get; set; }
        public string DataHora { get; set; }
        public int Status { get; set; }
        public string StatusDescricao { get; set; }
        public int Tipo { get; set; }
        public string TipoDescricao { get; set; }
        public string DescricaoSecundaria { get { return this.CriarDescricaoSecundaria(); }  }


        public static explicit operator ViewLoteVenda(Lote lote)
        {
            var view = new ViewLoteVenda
            {
                Id = lote.Id,
                Codigo = lote.Codigo,
                Tipo = lote.Tipo,
                Raca = "Lote de " + lote.Raca.Nome.ToLower(),
                DataHora = lote.DataHora.ToString("dd/MM/yyyy hh:mm"),
                Status = lote.Status,
                StatusDescricao = ((EStatusLote)lote.Status).Descricao()                
            };

            

            if (view.Tipo == (int)ETipoLote.Frigorificos)
            {
                view.TipoDescricao = "Venda para frigorífico";
            }
            else
            {
                view.TipoDescricao = "Venda para produtor";
            }
            
            if (lote.Etapas != null && lote.Etapas.Count > 1)
            {
                view.DataHora = lote.Etapas.OrderByDescending(x => x.DataHora).First().DataHora.ToString("dd/MM/yyyy hh:mm");
            }

            return view;
        }

        private string CriarDescricaoSecundaria()
        {
            if (this.Status == (int)EStatusLote.Ofertado)
            {
                return "Aguardando interessados";
            }

            if (this.Status == (int)EStatusLote.SelecionadoParaCompra)
            {
                return "Aguardando técnico";
            }

            if (this.Status == (int)EStatusLote.EncaminhadoParaAnaliseDoTecnico)
            {
                return "Aguardando liberação do técnico";
            }

            if (this.Status == (int)EStatusLote.LiberadoPeloTecnico)
            {
                return "Aguardando transporte";
            }

            return "";
        }

    }
}
