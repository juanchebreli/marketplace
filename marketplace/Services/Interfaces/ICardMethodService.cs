using marketplace.DTO.PaymentMethodDTO;
using marketplace.DTO.PaymentMethodDTO.CardMethodDTO;
using marketplace.Models;

namespace marketplace.Services.Interfaces
{
	public interface ICardMethodService
	{
		List<PaymentMethod> GetAll();
		PaymentMethod Get(int id);
		CardMethod Add(PaymentMethodCreateDTO entity);
		CardMethod Update(CardMethodUpdateDTO entity);
		void Delete(int id);
	}
}
