namespace marketplace.Models
{
	public class Free : State
	{

		public override bool canBeFree()
		{
			return false;
		}

		public override bool canBeReserved()
		{
			return true;
		}

		public override bool canBeSoldOut()
		{
			return true;
		}

		public override bool canBeShown()
		{
			return true;
		}

		public override void DoFree(ProductOnSale entity) { }

		public override void DoReserved(ProductOnSale entity)
		{

		}

		public override void DoSoldOut(ProductOnSale entity)
		{

		}
	}
}
