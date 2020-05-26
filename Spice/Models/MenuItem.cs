using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Spice.Models
{
	public class MenuItem
	{                              //Properties
		public int Id { get; set; }
		[Required]
		public string Name { get; set; }
		[Required]
		public string Description { get; set; }
		[Required]

		public string Spicyness { get; set; }
		public enum ESpicy { NA = 0, NotSpicy = 1, Spicy = 2, VerySpicy = 3 }

		public string Image { get; set; }                //store and upload image

		[Display(Name = "Category")]
		public int CategoryId { get; set; }

		[Foreignkey("CategoryId")]
		public virtual Category Category { get; set; }

		[Display(Name = "SubCategory")]
		public int SubCategoryId { get; set; }

		[Foreignkey("SubCategoryId")]
		public virtual SubCategory SubCategory { get; set; }

		[Range(50, int.MaxValue, ErrorMessage = "Price should be greater then Rupey 50.")]
		public double Price { get; set; }
	}
}
