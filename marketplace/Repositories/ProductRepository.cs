using marketplace.Context;
using marketplace.DTO.UserDTO;
using marketplace.Helpers;
using marketplace.Models;

namespace marketplace.Repositories
{
	public interface IProductRepository : IGenericRepository<Product>
	{
		public List<Product> GetAll();
		public bool FreeName(string name, int id);

	}
	public class ProductRepository : GenericRepository<Product, AppDbContext>, IProductRepository
	{
		readonly AppDbContext AppDbContext;

		public ProductRepository(AppDbContext dbContext) : base(dbContext)
		{
			AppDbContext = dbContext;
		}

		public List<Product> GetAll()
		{
			List<Product> users = AppDbContext.Products.Where(entity => entity.deleted == false).OrderBy(entity => entity.name).ToList();
			return users;
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
