using marketplace.MappingConfiguration;
using marketplace.DTO.CashMethodDTO;
using marketplace.Helpers;
using marketplace.Models;
using marketplace.Repositories;

namespace marketplace.Services
{
	public interface ICashMethodService
	{
		List<PaymentMethod> GetAll();
		PaymentMethod Get(int id);
		CashMethod Add(CashMethodCreateDTO entity);
		PaymentMethod Update(CashMethodUpdateDTO entity);
		List<string> Validations(int id);
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

		public CashMethod Add(CashMethodCreateDTO entity)
		{
			CashMethod cashMethod = CustomMapper.Map<CashMethodCreateDTO,CashMethod,CashMethodCreateDTO.MapperProfile>(entity);
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
