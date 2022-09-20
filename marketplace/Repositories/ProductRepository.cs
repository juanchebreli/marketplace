using marketplace.Context;
using marketplace.Models;
using marketplace.Repositories.Interfaces;

namespace marketplace.Repositories
{
	public class ProductRepository : GenericRepository<Product, AppDbContext>, IProductRepository
	{
		readonly AppDbContext AppDbContext;

		public ProductRepository(AppDbContext dbContext) : base(dbContext)
		{
			AppDbContext = dbContext;
		}

		public List<Product> GetAll()
		{
			List<Product> products = AppDbContext.Products.Where(entity => entity.deleted == false).OrderBy(entity => entity.name).ToList();
			return products;
		}

		public Product GetByName(string name)
		{
			return AppDbContext.Products.FirstOrDefault(c => c.name == name && c.deleted == false);
		}

	}
}
