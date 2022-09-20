using AutoMapper;
using marketplace.DTO.PaymentMethodDTO.CardMethodDTO;
using marketplace.DTO.PaymentMethodDTO.CashMethodDTO;
using marketplace.Helpers.Exceptions.Implements;
using marketplace.MappingConfiguration;
using marketplace.Models;
using marketplace.Repositories.Interfaces;
using marketplace.Services.Interfaces;
using System.Text;

namespace marketplace.Services
{
    public class PaymentService: IPaymentService
	{
		private readonly IPaymentRepository _paymentRepository;

		public PaymentService(IPaymentRepository paymentRepository)
		{

			_paymentRepository = paymentRepository;

		}

		public List<PaymentMethod> GetAll()
		{
			List<PaymentMethod> cardsMethod = _paymentRepository.GetAll();
			if (cardsMethod.Count == 0) throw new NoContentException("Don't have a payments");
			return cardsMethod;
		}

		public List<CashMethod> GetAllCash()
		{
			List<PaymentMethod> cashMethod = _paymentRepository.GetAllCard();
			if (cashMethod.Count == 0) throw new NoContentException("Don't have a payments method with cash");
			return this.mapCash<CashMethodDTO.MapperProfile>(cashMethod);
		}

		public List<CardMethod> GetAllCard()
		{
			List<PaymentMethod> cardsMethod = _paymentRepository.GetAllCard();
			if (cardsMethod.Count == 0) throw new NoContentException("Don't have a payments method with card");
			return this.mapCard<CardMethodDTO.MapperProfile>(cardsMethod);
		}

		public PaymentMethod Get(int id)
		{
			PaymentMethod cardMethod = _paymentRepository.Get(id);
			if (cardMethod == null) throw new NotFoundException(new StringBuilder("Not found a payment method with card with id: {0}", id).ToString());
			return cardMethod;
		}

		public CardMethod Add(CardMethod entity)
		{
			return _paymentRepository.AddCardMethod(entity);
		}

		public CashMethod Add(CashMethod entity)
		{
			return _paymentRepository.AddCashMethod(entity);
		}

		public CardMethod Update(CardMethodUpdateDTO entity)
		{
			CardMethod cardMethod = CustomMapper.Map<CardMethodUpdateDTO, CardMethod, CardMethodUpdateDTO.MapperProfile>(entity);
			return _paymentRepository.UpddateCardMethod(cardMethod);
		}

		public CashMethod Update(CashMethodUpdateDTO entity)
		{
			CashMethod cashMethod = CustomMapper.Map<CashMethodUpdateDTO, CashMethod, CashMethodUpdateDTO.MapperProfile>(entity);
			return _paymentRepository.UpddateCashMethod(cashMethod);
		}

		public void Delete(int id)
		{
			PaymentMethod payment = _paymentRepository.Get(id);
			if (payment == null) throw new NotFoundException(new StringBuilder("Not found a payment with id: {0}", id).ToString());

			payment.deleted = true;
			_paymentRepository.Update(payment);
		}

		#region private

		private List<CardMethod> mapCard<TMapperProfile>(List<PaymentMethod> cardMethods) where TMapperProfile : Profile, new()
		{
			return (List<CardMethod>)CustomMapper.Map<PaymentMethod, CardMethod, TMapperProfile>(cardMethods);
		}

		private List<CashMethod> mapCash<TMapperProfile>(List<PaymentMethod> cashMethods) where TMapperProfile : Profile, new()
		{
			return (List<CashMethod>)CustomMapper.Map<PaymentMethod, CashMethod, TMapperProfile>(cashMethods);
		}
		#endregion
	}
}
