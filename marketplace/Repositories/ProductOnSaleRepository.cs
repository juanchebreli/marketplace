using marketplace.Context;
using marketplace.Models;
using marketplace.Repositories.Interfaces;

namespace marketplace.Repositories
{
	public class ProductOnSaleRepository : GenericRepository<ProductOnSale, AppDbContext>, IProductOnSaleRepository
	{
		readonly AppDbContext AppDbContext;

		public ProductOnSaleRepository(AppDbContext dbContext) : base(dbContext)
		{
			AppDbContext = dbContext;
		}

		public List<ProductOnSale> GetAll()
		{
			List<ProductOnSale> productOnSales = AppDbContext.ProductsOnSale.Where(entity => entity.deleted == false).OrderBy(entity => entity.price).ToList();
			return productOnSales;
		}

	}
}
