using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Spice.Extentions
{
    /// <summary>
    /// items and its propertyName
    /// </summary>
    public static class ReflectionExtention
    {
        public static string GetpropertyValue<T>(this T item, string propertyName)
        {
            return item.GetType().GetProperty(propertyName).GetValue(item, null).ToString();
        }
    }
}
