using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Text;

namespace aoponto.lib
{
    public static class EnumExtensions
    {
        public static string Descricao<T>(this T e) where T : IConvertible
        {
            string description = null;

            if (e is Enum)
            {
                Type type = e.GetType();
                Array values = System.Enum.GetValues(type);

                foreach (int val in values)
                {
                    if (val == e.ToInt32(CultureInfo.InvariantCulture))
                    {
                        var memInfo = type.GetMember(type.GetEnumName(val));
                        var descriptionAttributes = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

                        if (descriptionAttributes.Length > 0)
                        {
                            description = ((DescriptionAttribute)descriptionAttributes[0]).Description;
                        }

                        break;
                    }
                }
            }

            
            return description;
        }

        public static string Titulo<T>(this T e) where T : IConvertible
        {
            string titulo = null;

            if (e is Enum)
            {
                Type type = e.GetType();
                Array values = System.Enum.GetValues(type);

                foreach (int val in values)
                {
                    if (val == e.ToInt32(CultureInfo.InvariantCulture))
                    {
                        var memInfo = type.GetMember(type.GetEnumName(val));
                        var displayNameAttributes = memInfo[0].GetCustomAttributes(typeof(DisplayNameAttribute), false);

                        if (displayNameAttributes.Length > 0)
                        {
                            titulo = ((DisplayNameAttribute)displayNameAttributes[0]).DisplayName;
                        }

                        break;
                    }
                }
            }

            return titulo;
        }

    }
}
