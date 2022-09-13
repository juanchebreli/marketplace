using marketplace.Models;

namespace marketplace.Repositories.Interfaces
{
	public interface IPurchaseRepository : IGenericRepository<Purchase>
	{
		List<Purchase> GetAll();
		Purchase GetByProductOnSale(int ProductOnSaleId);
	}
}
