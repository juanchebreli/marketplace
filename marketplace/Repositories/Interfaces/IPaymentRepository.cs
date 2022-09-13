using marketplace.Models;

namespace marketplace.Repositories.Interfaces
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
}
