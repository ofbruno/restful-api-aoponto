using aoponto.lib;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace aoponto.modelos
{
    public class ViewLoteConsulta
    {
        public int Id { get; set; }
        public DateTime DataHora { get; set; }
        public int Status { get; set; }
        public int Tipo { get; set; }
        public string Raca { get; set; }
        public string Genero { get; set; }
        public int Idade { get; set; }
        public int Quantidade { get; set; }
        public int PesoMedio { get; set; }
        public int Disponibilidade { get; set; }

        public decimal Preco { get; set; }
        public int Prazo { get; set; }
        public string TipoFrete { get; set; }
        public int DistanciaLimiteFrete { get; set; }
        public string Comentarios { get; set; }

        public string EnderecoNome { get; set; }
        public string EnderecoEstado { get; set; }
        public string EnderecoCidade { get; set; }
        public string EnderecoLogradouro { get; set; }
        public string EnderecoNumero { get; set; }
        public string EnderecoComplemento { get; set; }
        public string EnderecoCoordenadas { get; set; }

        public bool PodeSerSelecionado { get; set; }



        public static explicit operator ViewLoteConsulta(Lote lote)
        {
            var view = new ViewLoteConsulta
            {
                Id = lote.Id,
                Tipo = lote.Tipo,
                DataHora = lote.DataHora,
                Status = lote.Status,                                
                Genero = ((EGeneroLote)lote.Genero).Descricao(),
                Idade = lote.Idade,
                Quantidade = lote.Quantidade,
                PesoMedio = lote.PesoMedio,
                Disponibilidade = lote.Disponibilidade,
                Preco = lote.Preco,
                Prazo = lote.Prazo,
                TipoFrete = ((ETipoFreteLote)lote.TipoFrete).Descricao(),
                DistanciaLimiteFrete = (lote.DistanciaLimiteFrete.HasValue ? lote.DistanciaLimiteFrete.Value : 0),
                Comentarios = lote.Comentarios,
                PodeSerSelecionado = (lote.Status == (int)EStatusLote.Ofertado)
            };

            if (lote.Raca != null)
            {
                view.Raca = lote.Raca.Nome;
            }

            if (lote.Endereco != null)
            {
                view.EnderecoNome = lote.Endereco.Nome;
                view.EnderecoEstado = lote.Endereco.Estado.Sigla;
                view.EnderecoCidade = lote.Endereco.Cidade.Nome;
                view.EnderecoLogradouro = lote.Endereco.Logradouro;
                view.EnderecoNumero = lote.Endereco.Numero;
                view.EnderecoComplemento = lote.Endereco.Complemento;
                view.EnderecoCoordenadas = lote.Endereco.Coordenadas;
            }

            return view;
        }

    }
}
