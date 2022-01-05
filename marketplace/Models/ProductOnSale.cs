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

		public State GetState()
		{
			return StateFactory.GetState(this.stateName);
		}

	}
}
