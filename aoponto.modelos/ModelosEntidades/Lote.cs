using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace aoponto.modelos
{
    [Table("Lotes")]
    public partial class Lote
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public int Tipo { get; set; }
        public int UsuarioIdVendedor { get; set; }
        public int RacaId { get; set; }
        public int Disponibilidade { get; set; }
        public decimal Preco { get; set; }
        public int Prazo { get; set; }
        public int Genero { get; set; }
        public int Idade { get; set; }
        public int Quantidade { get; set; }
        public int TipoFrete { get; set; }
        public int? DistanciaLimiteFrete { get; set; }
        public int PesoMedio { get; set; }
        public int EnderecoId { get; set; }
        public string Comentarios { get; set; }
        public DateTime DataHora { get; set; }
        public int Status { get; set; }
        public int? UsuarioIdComprador { get; set; }

        public Usuario Vendedor { get; set; }
        public Usuario Comprador { get; set; }
        public Raca Raca { get; set; }
        public List<LoteRegistro> Registros { get; set; }
        public List<LoteAnexo> Anexos { get; set; }
        public Endereco Endereco { get; set; }
        public List<LoteEtapa> Etapas { get; set; }
    }
}
