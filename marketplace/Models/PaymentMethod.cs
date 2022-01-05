using Api.Models;

namespace marketplace.Models
{
	public abstract class PaymentMethod : Entity<int>
	{
		public string description { get; set; }

		public virtual Purchase Purchase { get; set; }
	}
}
