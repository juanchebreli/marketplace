namespace marketplace.Models
{
	// TPH hierarchy
	public abstract class PaymentMethod : Entity<int>
	{
		public string description { get; set; }
		public string type { get; set; }

		public virtual Purchase Purchase { get; set; }

		public static  CashMethod CASH { get; } = new CashMethod() { id = 1 , type = "CASH"};
		public static   CardMethod CARD { get; } = new CardMethod() { id = 2, type = "CARD" };

	}
}
