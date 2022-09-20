using marketplace.Helpers.Factory;
using marketplace.Helpers.States;
using System.ComponentModel.DataAnnotations;

namespace marketplace.Models
{
	public class ProductOnSale : Entity<int>
	{
		[Required]
		public decimal price { get; set; }

		[Required]
		public bool offer { get; set; }

		[Required]
		public int Userid { get; set; }

		public virtual User User { get; set; }

		[Required]
		public int Productid { get; set; }

		public virtual Product Product { get; set; }

		public virtual Purchase Purchase { get; set; }

		public int state { get; set; }

		public State GetState()
		{
			return StateFactory.GetState(state);
		}


	}
}
