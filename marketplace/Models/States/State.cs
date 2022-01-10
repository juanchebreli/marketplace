using System.ComponentModel.DataAnnotations.Schema;

namespace marketplace.Models
{
	public abstract class State
	{
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		public int id { get; set; }
		public abstract bool canBeShown();
		public abstract bool canBeFree();

		public abstract bool canBeSoldOut();
		public abstract bool canBeReserved();

		public abstract void DoFree(ProductOnSale entity);

		public abstract void DoSoldOut(ProductOnSale entity);

		public abstract void DoReserved(ProductOnSale entity);

		public static Free FREE { get; } = new Free() { id = 1 };
		public static Reserved RESERVED { get; } = new Reserved() { id = 2 };
		public static SoldOut SOLDOUT { get; } = new SoldOut() { id = 3};

		public static State GetState(int id)
		{
			switch (id)
			{
				case 1: return FREE;
				case 2: return RESERVED;
				case 3: return SOLDOUT;
				default: return null;						
			}
		}
	}
}
