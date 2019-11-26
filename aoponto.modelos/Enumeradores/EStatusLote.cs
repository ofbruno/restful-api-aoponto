using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace aoponto.modelos
{
    public enum EStatusLote
    {
        [Description("Oferta realizada")]
        Ofertado = 10,

        [Description("Selecionado para compra")]
        SelecionadoParaCompra = 20,

        [Description("Preço aceito")]
        PrecoAceitoPeloVendedor = 30,

        [Description("Em análise do técnico")]
        EncaminhadoParaAnaliseDoTecnico = 40,

        [Description("Liberado pelo técnico")]
        LiberadoPeloTecnico = 50,

        [Description("Aguardando transporte")]
        AguardandoTransporte = 60,

        [Description("Em transporte")]
        EmTransporte = 70,

        [Description("Chegou ao comprador")]
        ChegouAoComprador = 80
    }
}
