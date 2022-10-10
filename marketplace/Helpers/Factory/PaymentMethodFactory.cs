using marketplace.DTO.PaymentMethodDTO;
using marketplace.Helpers.Enums;
using marketplace.Helpers.Exceptions.Implements;
using marketplace.MappingConfiguration;
using marketplace.Models;
using marketplace.Services.Interfaces;
using System.Text;

namespace marketplace.Helpers.Factory
{
    public class PaymentMethodFactory
    {
		private readonly IPaymentService _paymentService;

		public PaymentMethodFactory(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        public PaymentMethod CreatePaymentMethod(PaymentMethodCreateDTO entity)
		{
			switch (entity.method)
			{
				case PaymentMethodsEnum.Cash:
					CashMethod cashMethod = CustomMapper.Map<PaymentMethodCreateDTO, CashMethod, PaymentMethodCreateDTO.MapperProfileCash>(entity);
					cashMethod.type = PaymentMethodsEnum.Cash.ToString();
					return _paymentService.Add(cashMethod);

				case PaymentMethodsEnum.Card:
					CardMethod cardMethod = CustomMapper.Map<PaymentMethodCreateDTO, CardMethod, PaymentMethodCreateDTO.MapperProfileCard>(entity);
					cardMethod.type = PaymentMethodsEnum.Card.ToString();
					return _paymentService.Add(cardMethod);

				default:
					throw new NotFoundException(new StringBuilder("Not found a payment method: " + entity.method).ToString());
			}
		}
	}
}
