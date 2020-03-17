using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Spice.Models.ViewModels
{
	public class MenuItemViewModel
	{
		public MenuItem MenuItem { get; set; }
		public IEnumerable<Category> Category { get; set; }
		public SelectList CategoryList { get; set; }
		public IEnumerable<SubCategory> SubCategory { get; set; }
	}
}
