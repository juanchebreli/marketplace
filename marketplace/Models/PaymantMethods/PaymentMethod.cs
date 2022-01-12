namespace marketplace.Models
{
	// TPH hierarchy
	public abstract class PaymentMethod : Entity<int>
	{
		public string description { get; set; }

		public virtual Purchase Purchase { get; set; }

		public const int CARDMETHOD = 1;

		public const  int CASHMETHOD = 2;

	}
}
