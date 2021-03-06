using marketplace.Context;
using marketplace.DTO.UserDTO;
using marketplace.Helpers;
using marketplace.Models;

namespace marketplace.Repositories
{
	public interface IPaymentRepository : IGenericRepository<PaymentMethod>
	{
		// CashMethod
		List<PaymentMethod> GetAllCash();
		CashMethod AddCashMethod(CashMethod cashMethod);
		CashMethod UpddateCashMethod(CashMethod cashMethod);


		//CardMethod
		List<PaymentMethod> GetAllCard();
		CardMethod AddCardMethod(CardMethod cashMethod);
		CardMethod UpddateCardMethod(CardMethod cashMethod);

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
			List<PaymentMethod> cashes = AppDbContext.PaymentMethods.Where(entity => (entity.type == PaymentMethod.CASH.type) && (entity.deleted == false)).ToList();
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

		public List<PaymentMethod> GetAllCard()
		{
			Type cash = typeof(CardMethod);
			List<PaymentMethod> cashes = AppDbContext.PaymentMethods.Where(entity => (entity.type == PaymentMethod.CASH.type) && (entity.deleted == false)).ToList();
			return cashes;
		}

		public CardMethod AddCardMethod(CardMethod cardMethod)
		{
			AppDbContext.PaymentMethods.Add(cardMethod);
			AppDbContext.SaveChanges();
			return cardMethod;
		}

		public CardMethod UpddateCardMethod(CardMethod cardMethod)
		{
			AppDbContext.PaymentMethods.Update(cardMethod);
			AppDbContext.SaveChanges();
			return cardMethod;
		}
	}
}
