using marketplace.MappingConfiguration;
using marketplace.Models;
using marketplace.Repositories;
using marketplace.DTO.PaymentMethodDTO.CardMethodDTO;

namespace marketplace.Services
{
	public interface ICardMethodService
	{
		List<PaymentMethod> GetAll();
		PaymentMethod Get(int id);
		CardMethod Add(string description);
		CardMethod Update(CardMethodUpdateDTO entity);
		void Delete(int id);
	}

	public class CardMethodService : ICardMethodService
	{
		private readonly IPaymentRepository _paymentRepository;
		private readonly IConfiguration _configuration;

		public CardMethodService(IPaymentRepository paymentRepository, IConfiguration configuration)
		{

			_paymentRepository = paymentRepository;
			_configuration = configuration;

		}

		public List<PaymentMethod> GetAll()
		{
			return _paymentRepository.GetAllCard();
		}

		public PaymentMethod Get(int id)
		{
			return _paymentRepository.Get(id);
		}

		public CardMethod Add(string _description)
		{
			CardMethod cardMethod = new CardMethod() { deleted = false, description = _description };
			return _paymentRepository.AddCardMethod(cardMethod);
		}

		public CardMethod Update(CardMethodUpdateDTO entity)
		{
			CardMethod cardMethod = CustomMapper.Map<CardMethodUpdateDTO, CardMethod, CardMethodUpdateDTO.MapperProfile>(entity);
			return _paymentRepository.UpddateCardMethod(cardMethod);
		}


		public void Delete(int id)
		{
			PaymentMethod payment = _paymentRepository.Get(id);
			payment.deleted = true;
			_paymentRepository.Update(payment);
		}
	}
}
