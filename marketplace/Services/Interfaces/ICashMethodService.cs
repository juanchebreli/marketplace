using marketplace.DTO.PaymentMethodDTO;
using marketplace.DTO.PaymentMethodDTO.CashMethodDTO;
using marketplace.Models;

namespace marketplace.Services.Interfaces
{
	public interface ICashMethodService
	{
		List<PaymentMethod> GetAll();
		PaymentMethod Get(int id);
		CashMethod Add(PaymentMethodCreateDTO entity);
		CashMethod Update(CashMethodUpdateDTO entity);
		void Delete(int id);
	}
}
