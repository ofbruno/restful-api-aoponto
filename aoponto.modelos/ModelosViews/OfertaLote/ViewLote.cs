using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace aoponto.modelos
{
    public class ViewLote
    {
        public int Id { get; set; }
        public DateTime DataHora { get; set; }
        public int Status { get; set; }

        [Required]
        public int Tipo { get; set; }

        [Required]
        public int RacaId { get; set; }

        [Required]
        public int Genero { get; set; }

        [Required]
        public int Idade { get; set; }

        [Required]
        public int Quantidade { get; set; }

        [Required]
        public int PesoMedio { get; set; }

        [Required]
        public int Disponibilidade { get; set; }

        public decimal Preco { get; set; }
        public int Prazo { get; set; }
        public int TipoFrete { get; set; }
        public List<string> Registros { get; set; }
        public int DistanciaLimiteFrete { get; set; }
        public string Comentarios { get; set; }
        public int EnderecoId { get; set; }
        public string EnderecoNome { get; set; }
        public int EnderecoEstadoId { get; set; }
        public int EnderecoCidadeId { get; set; }
        public string EnderecoLogradouro { get; set; }
        public string EnderecoNumero { get; set; }
        public string EnderecoComplemento { get; set; }
        public string EnderecoCoordenadas { get; set; }
        public bool EnderecoCoordenadasEndereco { get; set; }

        public static explicit operator ViewLote(Lote lote)
        {
            var view = new ViewLote
            {
                Id = lote.Id,
                Tipo = lote.Tipo,
                DataHora = lote.DataHora,
                Status = lote.Status,                
                RacaId = lote.RacaId,
                Genero = lote.Genero,
                Idade = lote.Idade,
                Quantidade = lote.Quantidade,
                PesoMedio = lote.PesoMedio,
                Disponibilidade = lote.Disponibilidade,
                Preco = lote.Preco,
                Prazo = lote.Prazo,
                TipoFrete = lote.TipoFrete,
                DistanciaLimiteFrete = (lote.DistanciaLimiteFrete.HasValue ? lote.DistanciaLimiteFrete.Value : 0),
                EnderecoId = lote.EnderecoId,
                Comentarios = lote.Comentarios
            };

            if (lote.Endereco != null)
            {
                view.EnderecoNome = lote.Endereco.Nome;
                view.EnderecoEstadoId = lote.Endereco.EstadoId;
                view.EnderecoCidadeId = lote.Endereco.CidadeId;
                view.EnderecoLogradouro = lote.Endereco.Logradouro;
                view.EnderecoNumero = lote.Endereco.Numero;
                view.EnderecoComplemento = lote.Endereco.Complemento;
                view.EnderecoCoordenadas = lote.Endereco.Coordenadas;
                view.EnderecoCoordenadasEndereco = lote.Endereco.CoordenadasEndereco;
            }

            if (lote.Registros != null)
            {
                view.Registros = new List<string>();
                lote.Registros.ForEach(x => view.Registros.Add(x.Registro));
            }

            return view;
        }

        public Lote ObterLoteInclusao(int usuarioId)
        {
            var lote = (Lote)this;

            if (string.IsNullOrEmpty(lote.Endereco.Nome))
            {
                lote.Endereco.Nome = lote.Endereco.Logradouro;
            }

            lote.Codigo = usuarioId.ToString().PadLeft(4, '0') + lote.DataHora.ToString("yyMM") + lote.Genero.ToString() + lote.Quantidade.ToString();
            lote.UsuarioIdVendedor = usuarioId;
            lote.DataHora = DateTime.Now;
            lote.Status = (int)EStatusLote.Ofertado;
            lote.Etapas = new List<LoteEtapa>();
            lote.Etapas.Add(new LoteEtapa
            {
                DataHora = lote.DataHora,
                Status = (int)EStatusLote.Ofertado,
                UsuarioId = lote.UsuarioIdVendedor
            });

            return lote;
        }
    }
}
