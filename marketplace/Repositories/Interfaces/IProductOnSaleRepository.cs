using marketplace.Models;

namespace marketplace.Repositories.Interfaces
{
	public interface IProductOnSaleRepository : IGenericRepository<ProductOnSale>
	{
		public List<ProductOnSale> GetAll();

	}
}
