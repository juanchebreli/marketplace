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

		public bool FreeName(string name, int id)
		{
			Product entity = AsNoTracking().FirstOrDefault(c => c.name == name && c.deleted == false);

			if (entity != null && entity.id == id)
				return true;
			else
				return (entity == null);
		}

	}
}
