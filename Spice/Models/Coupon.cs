using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Spice.Models
{
	public class Coupon
	{
		[Key]  // Attributes
		public int Id { get; set; }  // Property

		[Required]
		[MaxLength(50)]
		public string Name { get; set; }

		[Required]
		[MaxLength(40)]
		public string CouponType { get; set; }
		public enum ECouponType { Percent = 0, Rupey = 50 }
		[Required]
		public double Discount { get; set; }
		[Required]
		public double MinimumAmount { get; set; }
		public byte[] Picture { get; set; }
		public bool IsActive { get; set; }
	}
}
