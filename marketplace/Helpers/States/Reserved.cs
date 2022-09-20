using marketplace.Helpers.Enums;
using marketplace.Models;

namespace marketplace.Helpers.States
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

		public override void DoFree(ProductOnSale entity) {
			entity.state = (int)StatesEnum.FREE;
		}

		public override void DoReserved(ProductOnSale entity){}

		public override void DoSoldOut(ProductOnSale entity)
		{
			entity.state = (int)StatesEnum.SOLDOUT;
		}
	}
}
