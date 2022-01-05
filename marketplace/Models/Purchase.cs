using Api.Models;
using System.ComponentModel.DataAnnotations;

namespace marketplace.Models
{
	public class Purchase : Entity<int>
	{
		[Required]
		public decimal amount { get; set; }

		[Required]
		public DateTime date { get; set; }

		[Required]
		public int Userid { get; set; }

		public virtual User User { get; set; }

		[Required]
		public int ProductOnSaleid { get; set; }

		public virtual ProductOnSale ProductOnSale { get; set; }

		[Required]
		public int PaymentMethodid { get; set; }

		public virtual PaymentMethod PaymentMethod { get; set; }
	}
}
