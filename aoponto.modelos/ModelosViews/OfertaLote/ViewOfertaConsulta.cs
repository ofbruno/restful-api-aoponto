using aoponto.lib;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace aoponto.modelos
{
    public class ViewOfertaConsulta
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Raca { get; set; }
        public string Genero { get; set; }
        public string DataHora { get; set; }
        public int Status { get; set; }
        public string StatusDescricao { get; set; }
        public int Tipo { get; set; }
        public string TipoDescricao { get; set; }
        public string Localizacao { get; set; }


        public static explicit operator ViewOfertaConsulta(Lote lote)
        {
            var view = new ViewOfertaConsulta
            {
                Id = lote.Id,
                Codigo = lote.Codigo,
                Tipo = lote.Tipo,
                Raca = "Lote de " + lote.Raca.Nome.ToLower(),
                Genero =((EGeneroLote)lote.Genero).Descricao(),
                DataHora = lote.DataHora.ToString("dd/MM/yyyy hh:mm"),
                Status = lote.Status,
                StatusDescricao = ((EStatusLote)lote.Status).Descricao(),
                TipoDescricao = ((ETipoLote)lote.Tipo).Descricao(),
                Localizacao = lote.Endereco.Cidade.Nome + "/" + lote.Endereco.Estado.Sigla
            };

            if (view.Tipo == (int)ETipoLote.Frigorificos)
            {
                view.TipoDescricao = "Destinado para frigoríficos";
            }
            else
            {
                view.TipoDescricao = "Destinado para outros produtores";
            }
            

            return view;
        }

    }
}
