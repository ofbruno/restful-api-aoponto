using System;
using System.Collections.Generic;
using System.Text;

namespace aoponto.modelos
{
    public class ViewNotificacao
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Mensagem { get; set; }
        public DateTime DataHora { get; set; }
        public int Tipo { get; set; }
        public string Parametros { get; set; }
        public bool Lido { get; set; }

        public static explicit operator ViewNotificacao(Notificacao notificacao)
        {
            var view = new ViewNotificacao()
            {
                Id = notificacao.Id,
                Tipo = notificacao.Tipo,
                Mensagem = notificacao.Mensagem,
                Titulo = notificacao.Titulo,
                Parametros = notificacao.Parametros,
                DataHora = notificacao.DataHora,
                Lido = (notificacao.DataHoraLeitura > new DateTime(1900,1,1))
            };

            return view;
        }
    }
}
