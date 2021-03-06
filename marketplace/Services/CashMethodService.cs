using marketplace.MappingConfiguration;
using marketplace.DTO.PaymentMethodDTO.CashMethodDTO;
using marketplace.Helpers;
using marketplace.Models;
using marketplace.Repositories;
using marketplace.DTO.PaymentMethodDTO;

namespace marketplace.Services
{
	public interface ICashMethodService
	{
		List<PaymentMethod> GetAll();
		PaymentMethod Get(int id);
		CashMethod Add(PaymentMethodCreateDTO entity);
		CashMethod Update(CashMethodUpdateDTO entity);
		void Delete(int id);
	}

	public class CashMethodService : ICashMethodService
	{
		private readonly IPaymentRepository _paymentRepository;
		private readonly IConfiguration _configuration;

		public CashMethodService(IPaymentRepository paymentRepository, IConfiguration configuration)
		{

			_paymentRepository = paymentRepository;
			_configuration = configuration;

		}

		public List<PaymentMethod> GetAll()
		{
			return _paymentRepository.GetAllCash();
		}

		public PaymentMethod Get(int id)
		{
			return _paymentRepository.Get(id);
		}

		public CashMethod Add(PaymentMethodCreateDTO entity)
		{
			CashMethod cashMethod = CustomMapper.Map<PaymentMethodCreateDTO, CashMethod, PaymentMethodCreateDTO.MapperProfileCash>(entity);
			cashMethod.type = PaymentMethod.CASH.type;
			return _paymentRepository.AddCashMethod(cashMethod);
		}

		public CashMethod Update(CashMethodUpdateDTO entity)
		{
			CashMethod cashMethod = CustomMapper.Map<CashMethodUpdateDTO, CashMethod, CashMethodUpdateDTO.MapperProfile>(entity);
			return _paymentRepository.UpddateCashMethod(cashMethod);
		}


		public void Delete(int id)
		{
			PaymentMethod payment = _paymentRepository.Get(id);
			payment.deleted = true;
			_paymentRepository.Update(payment);
		}
	}
}
