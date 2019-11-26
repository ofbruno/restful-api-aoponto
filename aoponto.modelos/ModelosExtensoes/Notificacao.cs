using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace aoponto.modelos
{   
    public partial class Notificacao
    {
        [Column(name: "DataHoraLeitura"), NotMapped]
        public DateTime DataHoraLeitura { get; set; }
    }
}
