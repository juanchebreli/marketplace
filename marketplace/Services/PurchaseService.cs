using marketplace.MappingConfiguration;
using marketplace.DTO.PurchaseDTO;
using marketplace.Models;
using Microsoft.EntityFrameworkCore.Storage;
using marketplace.Context;
using marketplace.DTO.PaymentMethodDTO;
using marketplace.Services.Interfaces;
using marketplace.Repositories.Interfaces;

namespace marketplace.Services
{
	public class PurchaseService : IPurchaseService
	{
		private readonly IPurchaseRepository _purchaseRepository;
		private readonly IConfiguration _configuration;
		private readonly IUserService _userService;
		private readonly IProductOnSaleService _productOnSaleserService;
		private readonly ICashMethodService _cashMethodService;
		private readonly ICardMethodService _cardMethodService;
		private readonly AppDbContext AppDbContext;


		public PurchaseService(IPurchaseRepository purchaseRepository, IConfiguration configuration, IUserService userService,
			IProductOnSaleService productOnSaleserService, ICashMethodService cashMethodService,
			ICardMethodService cardMethodService, AppDbContext dbContext)
		{

			_purchaseRepository = purchaseRepository;
			_configuration = configuration;
			_userService = userService;
			_productOnSaleserService = productOnSaleserService;
			_cashMethodService = cashMethodService;
			_cardMethodService = cardMethodService;
			AppDbContext = dbContext;

		}

		public List<Purchase> GetAll()
		{
			return _purchaseRepository.GetAll();
		}

		public Purchase Get(int id)
		{
			return _purchaseRepository.Get(id);
		}

		public Purchase Add(PurchaseCreateDTO entity)
		{
			using (IDbContextTransaction transaction = this.AppDbContext.Database.BeginTransaction())
			{
				try
				{
					PaymentMethod paymentMethod = this.CreatePaymentMethod(entity.paymentMethod);
					entity.PaymentMethodid = paymentMethod.id;
					Purchase purchase = CustomMapper.Map<PurchaseCreateDTO, Purchase, PurchaseCreateDTO.MapperProfile>(entity);
					_purchaseRepository.Add(purchase);
					transaction.Commit();
					return(purchase);
				}
				catch (Exception ex)
				{
					transaction.Rollback();
					return null;
				}
			}
		}

		public Purchase Update(PurchaseUpdateDTO entity)
		{
			return _purchaseRepository.Update<PurchaseUpdateDTO, PurchaseUpdateDTO.MapperProfile>(entity);
		}

		public Purchase Update(Purchase entity)
		{
			return _purchaseRepository.Update(entity);
		}
		public List<string> Validations(int Userid, int ProductOnSaleid, int paymentMethod, int id)
		{
			List<string> errors = new List<string>();
			if (_userService.Get(Userid) == null)
				errors.Add("User dont' exist");
			if (_productOnSaleserService.Get(ProductOnSaleid) == null)
				errors.Add("Product On Sale dont' exist");
			if (_purchaseRepository.GetByProductOnSale(ProductOnSaleid) != null)
				errors.Add("Product On Sale already have a sale");
			if ((paymentMethod != PaymentMethod.CASH.id) && (paymentMethod != PaymentMethod.CARD.id))
				errors.Add("Payment method is invalid");
			return errors;
		}


		public void Delete(int id)
		{
			Purchase purchase = _purchaseRepository.Get(id);
			purchase.deleted = true;
			_purchaseRepository.Update(purchase);
		}

		public PaymentMethod CreatePaymentMethod(PaymentMethodCreateDTO paymentMethodCreateDTO)
		{

			// I don't use case becouse id needs to be constant
			if (paymentMethodCreateDTO.method == PaymentMethod.CASH.id)
			{
				return _cashMethodService.Add(paymentMethodCreateDTO);
			}
			else
			{
				if (paymentMethodCreateDTO.method == PaymentMethod.CARD.id)
				{
					return _cardMethodService.Add(paymentMethodCreateDTO);
				}
			}
			return null;
		}


	}
}
