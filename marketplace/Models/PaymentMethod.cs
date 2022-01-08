namespace marketplace.Models
{
	// TPH hierarchy
	public abstract class PaymentMethod : Entity<int>
	{
		public string description { get; set; }

		public virtual Purchase Purchase { get; set; }
	}
}
