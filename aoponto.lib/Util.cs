using System;

namespace aoponto.lib
{
    public static class Util
    {
        public static int GerarTokenNumerico()
        {
            return 123456;
            var random = new Random();
            var byteArray = new byte[9];
            var token = "";

            random.NextBytes(byteArray);

            for (int i = 0; i < byteArray.Length; i++)
            {
                token = token + byteArray.GetValue(i).ToString();                
            }

            return int.Parse(token.Substring(0,9));
        }

        public static string GerarDescricaoTempo(DateTime dataHora)
        {
            return dataHora.ToString("dd/MM/yyyy hh:mm");
        }
    }
}
