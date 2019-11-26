using aoponto.lib;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace aoponto.modelos
{
    public class ViewLoteDetalhes
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string DataHora { get; set; }
        public int Status { get; set; }
        public string StatusDescricao { get; set; }
        public int Tipo { get; set; }
        public string TipoDescricao { get; set; }
        public string Raca { get; set; }
        public string Genero { get; set; }
        public string Idade { get; set; }
        public string Quantidade { get; set; }
        public int PesoMedio { get; set; }
        public string Disponibilidade { get; set; }

        public string Preco { get; set; }
        public string Prazo { get; set; }
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

        public List<string> Anexos { get; set; }
        public List<ViewLoteEtapaVenda> EtapasVenda { get; set; }
        public List<ViewLoteEtapaCompra> EtapasCompra { get; set; }

        public static explicit operator ViewLoteDetalhes(Lote lote)
        {
            var view = new ViewLoteDetalhes
            {
                Id = lote.Id,
                Codigo = lote.Codigo,
                Tipo = lote.Tipo,
                DataHora = lote.DataHora.ToString("dd/MM/yyyy hh:mm"),
                Status = lote.Status,
                StatusDescricao = ((EStatusLote)lote.Status).Descricao(),
                Genero = ((EGeneroLote)lote.Genero).Descricao(),
                Idade = lote.Idade.ToString() + " meses",
                Quantidade = lote.Quantidade.ToString() + " animais",
                PesoMedio = lote.PesoMedio,                
                DistanciaLimiteFrete = (lote.DistanciaLimiteFrete.HasValue ? lote.DistanciaLimiteFrete.Value : 0),
                Comentarios = lote.Comentarios,
                PodeSerSelecionado = (lote.Status == (int)EStatusLote.Ofertado)
            };

            view.TipoFrete = ((ETipoFreteLote)lote.TipoFrete).Descricao();
            view.TipoFrete = string.Format(view.TipoFrete, view.DistanciaLimiteFrete.ToString());

            if(lote.Preco == 0)
            {
                view.Preco = "Aceita oferta";
            }
            else
            {
                view.Preco = "R$ " + lote.Preco.ToString("0.00");
            }

            if (lote.Disponibilidade <= 1)
            {
                view.Disponibilidade = "Imediata";
            }
            else
            {
                view.Disponibilidade = lote.Disponibilidade.ToString() + " dias";
            }

            if (lote.Prazo <= 1)
            {
                view.Prazo = "A vista";
            }
            else
            {
                view.Prazo = lote.Prazo.ToString() + " dias";
            }

            if (lote.Raca != null)
            {
                view.Raca = lote.Raca.Nome;
            }

            if (view.Tipo == (int)ETipoLote.Frigorificos)
            {
                view.TipoDescricao = "Frigoríficos";
            }
            else
            {
                view.TipoDescricao = "Outros produtores";
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

            if (lote.Anexos != null)
            {
                view.Anexos = new List<string>();
                lote.Anexos.ForEach(a => view.Anexos.Add(a.Url));
            }

            view.EtapasVenda = new List<ViewLoteEtapaVenda>();
            view.EtapasCompra = new List<ViewLoteEtapaCompra>();

            return view;
        }        
    }
}
