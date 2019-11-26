using System;
using System.Collections.Generic;
using System.Text;

namespace aoponto.modelos
{
    public partial class Lote
    {
        public static explicit operator Lote(ViewLote view)
        {
            var lote = new Lote
            {
                Id = view.Id,
                Tipo = view.Tipo,
                DataHora = view.DataHora,
                Status = view.Status,
                RacaId = view.RacaId,
                Preco = view.Preco,
                Prazo = view.Prazo,
                Disponibilidade = view.Disponibilidade,
                Genero = view.Genero,
                Idade = view.Idade,
                Quantidade = view.Quantidade,
                PesoMedio = view.PesoMedio,
                TipoFrete = view.TipoFrete,
                DistanciaLimiteFrete = view.DistanciaLimiteFrete,
                EnderecoId = view.EnderecoId,
                Comentarios = view.Comentarios,
                Endereco = new Endereco
                {
                    Id = view.EnderecoId,
                    Nome = view.EnderecoNome,
                    Logradouro = view.EnderecoLogradouro,
                    Numero = view.EnderecoNumero,
                    Complemento = view.EnderecoComplemento,
                    CidadeId = view.EnderecoCidadeId,
                    EstadoId = view.EnderecoEstadoId,
                    Coordenadas = view.EnderecoCoordenadas,
                    CoordenadasEndereco = view.EnderecoCoordenadasEndereco
                }
            };

            if (view.Registros != null)
            {
                lote.Registros = new List<LoteRegistro>();

                view.Registros.ForEach(x => 
                {
                    if (x != null && x.Trim().Length > 0)
                    {
                        lote.Registros.Add(new LoteRegistro() { Registro = x });
                    }
                });
            }

            return lote;
        }
    }
}
