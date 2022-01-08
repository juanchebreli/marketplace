using marketplace.Context;
using marketplace.DTO.UserDTO;
using marketplace.Helpers;
using marketplace.Models;

namespace marketplace.Repositories
{
	public interface IPaymentRepository : IGenericRepository<PaymentMethod>
	{
		public List<PaymentMethod> GetAllCash();
		public CashMethod AddCashMethod(CashMethod cashMethod);
		public CashMethod UpddateCashMethod(CashMethod cashMethod);

	}
	public class PaymentRepository : GenericRepository<PaymentMethod, AppDbContext>, IPaymentRepository
	{
		readonly AppDbContext AppDbContext;

		public PaymentRepository(AppDbContext dbContext) : base(dbContext)
		{
			AppDbContext = dbContext;
		}

		public List<PaymentMethod> GetAllCash()
		{
			Type cash = typeof(CashMethod);
			List<PaymentMethod> cashes = AppDbContext.PaymentMethods.Where(entity =>  (entity.GetType().Equals(cash)) && (entity.deleted == false)).ToList();
			return cashes;
		}

		public CashMethod AddCashMethod(CashMethod cashMethod)
		{
			AppDbContext.PaymentMethods.Add(cashMethod);
			AppDbContext.SaveChanges();
			return cashMethod;
		}

		public CashMethod UpddateCashMethod(CashMethod cashMethod)
		{
			AppDbContext.PaymentMethods.Update(cashMethod);
			AppDbContext.SaveChanges();
			return cashMethod;
		}

	}
}
