using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Spice.Extentions
{
    public static class IEnumerableExtention
    {
        /// <summary>
        /// SelectedList item for Category
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items"></param>
        /// <param name="selectedValue"></param>
        /// <returns>items</returns>
        public static IEnumerable<SelectListItem> ToSelectListItem<T>(this IEnumerable<T> items, int selectedValue)
        {
            return from item in items
                   select new SelectListItem
                   {
                       Text = item.GetpropertyValue("Name"),
                       Value = item.GetpropertyValue("Id"),
                       Selected = item.GetpropertyValue("Id").Equals(selectedValue.ToString())
                   };
        }
    }
}