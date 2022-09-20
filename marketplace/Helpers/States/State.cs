using marketplace.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace marketplace.Helpers.States
{
	public abstract class State
	{
		public int id { get; set; }
		public abstract bool canBeShown();
		public abstract bool canBeFree();

		public abstract bool canBeSoldOut();
		public abstract bool canBeReserved();

		public abstract void DoFree(ProductOnSale entity);

		public abstract void DoSoldOut(ProductOnSale entity);

		public abstract void DoReserved(ProductOnSale entity);
	}
}
