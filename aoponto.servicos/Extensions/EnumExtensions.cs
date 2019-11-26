using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Text;

namespace aoponto.servicos
{
    //public static class EnumExtensions
    //{
    //    public static string Descricao<T>(this T e) where T : IConvertible
    //    {
    //        string description = null;

    //        if (e is Enum)
    //        {
    //            Type type = e.GetType();
    //            Array values = System.Enum.GetValues(type);

    //            foreach (int val in values)
    //            {
    //                if (val == e.ToInt32(CultureInfo.InvariantCulture))
    //                {
    //                    var memInfo = type.GetMember(type.GetEnumName(val));
    //                    var descriptionAttributes = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

    //                    if (descriptionAttributes.Length > 0)
    //                    {
    //                        description = ((DescriptionAttribute)descriptionAttributes[0]).Description;
    //                    }

    //                    break;
    //                }
    //            }
    //        }

    //        return description;
    //    }

    //    public static string Titulo<T>(this T e) where T : IConvertible
    //    {
    //        string titulo = null;

    //        if (e is Enum)
    //        {
    //            Type type = e.GetType();
    //            Array values = System.Enum.GetValues(type);

    //            foreach (int val in values)
    //            {
    //                if (val == e.ToInt32(CultureInfo.InvariantCulture))
    //                {
    //                    var memInfo = type.GetMember(type.GetEnumName(val));
    //                    var displayNameAttributes = memInfo[0].GetCustomAttributes(typeof(DisplayNameAttribute), false);

    //                    if (displayNameAttributes.Length > 0)
    //                    {
    //                        titulo = ((DisplayNameAttribute)displayNameAttributes[0]).DisplayName;
    //                    }

    //                    break;
    //                }
    //            }
    //        }

    //        return titulo;
    //    }

    //}

    public static class EnumNotificacaoExtensions
    {
        public static string MensagemPush(this ETipoNotificacao notificacao)
        {
            switch (notificacao)
            {
                case ETipoNotificacao.NovaOfertaLoteParaFrigorificos:
                    return "Um novo lote de animais acaba de ser ofertado por um produtor.";

                case ETipoNotificacao.NovaOfertaLoteParaProdutores:
                    return "Um novo lote de animais acaba de ser ofertado por um produtor.";
            }

            return "";
        }

        public static string MensagemNotificacao(this ETipoNotificacao notificacao)
        {
            switch (notificacao)
            {
                case ETipoNotificacao.NovaOfertaLoteParaFrigorificos:
                    return "Um lote de animais foi ofertado por um produtor.";

                case ETipoNotificacao.NovaOfertaLoteParaProdutores:
                    return "Um lote de animais foi ofertado por um produtor.";
            }

            return "";
        }

        public static string TituloNotificacao(this ETipoNotificacao notificacao)
        {
            switch (notificacao)
            {
                case ETipoNotificacao.NovaOfertaLoteParaFrigorificos:
                    return "Oferta de lote";

                case ETipoNotificacao.NovaOfertaLoteParaProdutores:
                    return "Oferta de lote";
            }

            return "";
        }

        public static string TituloPush(this ETipoNotificacao notificacao)
        {
            switch (notificacao)
            {
                case ETipoNotificacao.NovaOfertaLoteParaFrigorificos:
                    return "Oferta de lote";

                case ETipoNotificacao.NovaOfertaLoteParaProdutores:
                    return "Oferta de lote";
            }

            return "";
        }
    }
}
