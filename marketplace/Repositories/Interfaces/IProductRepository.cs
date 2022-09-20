using marketplace.Models;

namespace marketplace.Repositories.Interfaces
{
	public interface IProductRepository : IGenericRepository<Product>
	{
		List<Product> GetAll();
		Product GetByName(string name);
	}
}
