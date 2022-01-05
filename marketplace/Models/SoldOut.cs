namespace marketplace.Models
{
	public class SoldOut : State
	{

		public override bool canBeFree()
		{
			return false;
		}

		public override bool canBeReserved()
		{
			return false;
		}

		public override bool canBeShown()
		{
			return false;
		}

		public override bool canBeSoldOut()
		{
			return false;
		}

		public override void DoFree(ProductOnSale entity)
		{
			entity.stateName = typeof(Free).Name;
		}

		public override void DoReserved(ProductOnSale entity)
		{
			entity.stateName = typeof(Reserved).Name;
		}

		public override void DoSoldOut(ProductOnSale entity) { }
	}
}
