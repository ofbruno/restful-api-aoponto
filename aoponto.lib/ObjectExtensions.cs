using System;
using System.Collections.Generic;
using System.Text;

namespace aoponto.lib
{
    public static class ObjectExtensions
    {
        public static object GetProperty<T>(this T obj, string name) where T : class
        {
            Type t = typeof(T);
            return t.GetProperty(name).GetValue(obj, null);
        }
                
        public static int GetPropertyInt<T>(this T obj, string name) where T : class
        {
            Type t = typeof(T);
            return (int)t.GetProperty(name).GetValue(obj, null);
        }
    }
}
