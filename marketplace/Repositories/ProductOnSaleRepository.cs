using marketplace.Context;
using marketplace.DTO.UserDTO;
using marketplace.Helpers;
using marketplace.Models;

namespace marketplace.Repositories
{
	public interface IProductOnSaleRepository : IGenericRepository<ProductOnSale>
	{
		public List<ProductOnSale> GetAll();

	}
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
