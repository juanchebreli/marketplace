using marketplace.Context;
using marketplace.Models;
using marketplace.Repositories.Interfaces;

namespace marketplace.Repositories
{
	public class PurchaseRepository : GenericRepository<Purchase, AppDbContext>, IPurchaseRepository
	{
		readonly AppDbContext AppDbContext;

		public PurchaseRepository(AppDbContext dbContext) : base(dbContext)
		{
			AppDbContext = dbContext;
		}

		public List<Purchase> GetAll()
		{
			List<Purchase> purchases = AppDbContext.Purchases.Where(entity => entity.deleted == false).OrderBy(entity => entity.date).ToList();
			return purchases;
		}

		public Purchase GetByProductOnSale(int ProductOnSaleId)
		{
			Purchase purchase = AppDbContext.Purchases.FirstOrDefault(purchase => purchase.ProductOnSaleid == ProductOnSaleId);
			return purchase;
		}
	}
}
