using Api.Models;
using marketplace.Helpers;
using System.ComponentModel.DataAnnotations;

namespace marketplace.Models
{
	public class ProductOnSale : Entity<int>
	{
		[Required]
		public decimal price { get; set; }

		[Required]
		public string stateName { get; set; }

		[Required]
		public bool offer { get; set; }

		[Required]
		public int Userid { get; set; }

		public virtual User User { get; set; }

		[Required]
		public int Productid { get; set; }

		public virtual Product Product { get; set; }

		public virtual Purchase Purchase { get; set; }

		public State GetState()
		{
			return StateFactory.GetState(this.stateName);
		}

	}
}
