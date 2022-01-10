namespace marketplace.Models
{
	public class Reserved : State
	{

		public override bool canBeFree()
		{
			return true;
		}

		public override bool canBeReserved()
		{
			return false;
		}

		public override bool canBeSoldOut()
		{
			return true;
		}

		public override bool canBeShown()
		{
			return false;
		}

		public override void DoFree(ProductOnSale entity)
		{

		}

		public override void DoReserved(ProductOnSale entity) { }

		public override void DoSoldOut(ProductOnSale entity)
		{

		}
	}
}
