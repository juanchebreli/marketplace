using marketplace.DTO.PaymentMethodDTO;
using marketplace.DTO.PurchaseDTO;
using marketplace.Models;

namespace marketplace.Services.Interfaces
{
	public interface IPurchaseService
	{
		List<Purchase> GetAll();
		Purchase Get(int id);
		Purchase Add(PurchaseCreateDTO entity);
		Purchase Update(PurchaseUpdateDTO entity);
		Purchase Update(Purchase entity);
		List<string> Validations(int Userid, int ProductOnSaleid, int paymentMethod, int id);
		void Delete(int id);
		PaymentMethod CreatePaymentMethod(PaymentMethodCreateDTO paymentMethodCreateDTO);
	}
}
