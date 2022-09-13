using marketplace.Models;

namespace marketplace.Repositories.Interfaces
{
	public interface IProductRepository : IGenericRepository<Product>
	{
		public List<Product> GetAll();
		public bool FreeName(string name, int id);

	}
}
