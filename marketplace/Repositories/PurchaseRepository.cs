using marketplace.Context;
using marketplace.DTO.UserDTO;
using marketplace.Helpers;
using marketplace.Models;

namespace marketplace.Repositories
{
	public interface IPurchaseRepository : IGenericRepository<Purchase>
	{
		public List<Purchase> GetAll();

	}
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

	}
}
