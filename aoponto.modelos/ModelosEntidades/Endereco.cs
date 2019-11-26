using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace aoponto.modelos
{
    [Table("Enderecos")]
    public class Endereco
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public string Nome { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public int CidadeId { get; set; }
        public int EstadoId { get; set; }
        public string Coordenadas { get; set; }
        public bool CoordenadasEndereco { get; set; }

        public Usuario Usuario { get; set; }
        public Cidade Cidade { get; set; }
        public Estado Estado { get; set; }
    }
}
