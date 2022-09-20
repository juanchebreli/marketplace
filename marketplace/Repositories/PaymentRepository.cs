using marketplace.Context;
using marketplace.Helpers.Enums;
using marketplace.Models;
using marketplace.Repositories.Interfaces;

namespace marketplace.Repositories
{
	public class PaymentRepository : GenericRepository<PaymentMethod, AppDbContext>, IPaymentRepository
	{
		readonly AppDbContext AppDbContext;

		public PaymentRepository(AppDbContext dbContext) : base(dbContext)
		{
			AppDbContext = dbContext;
		}

		public List<PaymentMethod> GetAll()
		{
			List<PaymentMethod> paymentMethods = AppDbContext.PaymentMethods.Where(entity => entity.deleted == false).ToList();
			return paymentMethods;
		}

		public List<PaymentMethod> GetAllCash()
		{
			List<PaymentMethod> cashMethods = AppDbContext.PaymentMethods.Where(entity => (entity.type == PaymentMethodsEnum.Cash.ToString()) && (entity.deleted == false)).ToList();
			return cashMethods;
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
			List<PaymentMethod> cardMethods = AppDbContext.PaymentMethods.Where(entity => (entity.type == PaymentMethodsEnum.Card.ToString()) && (entity.deleted == false)).ToList();
			return cardMethods;
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
