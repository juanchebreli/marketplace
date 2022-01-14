using marketplace.MappingConfiguration;
using AutoMapper;
using marketplace.DTO.PurchaseDTO;
using marketplace.Helpers;
using marketplace.Models;
using marketplace.Repositories;
using Microsoft.EntityFrameworkCore.Storage;
using marketplace.Context;

namespace marketplace.Services
{
	public interface IPurchaseService
	{
		List<Purchase> GetAll();
		Purchase Get(int id);
		Purchase Add(PurchaseCreateDTO entity);
		Purchase Update(PurchaseUpdateDTO entity);
		Purchase Update(Purchase entity);
		List<string> Validations(int Userid, int ProductOnSaleid, int id);
		void Delete(int id);
		PaymentMethod CreatePaymentMethod(int paymentMethod, string description);
	}

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
					PaymentMethod paymentMethod = this.CreatePaymentMethod(entity.PaymentMethod, entity.description);
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
		public List<string> Validations(int Userid, int ProductOnSaleid, int id)
		{
			List<string> errors = new List<string>();
			if (_userService.Get(Userid) == null)
				errors.Add("User dont' exist");
			if (_productOnSaleserService.Get(ProductOnSaleid) == null)
				errors.Add("Product On Sale dont' exist");
			if (_purchaseRepository.GetByProductOnSale(ProductOnSaleid) == null)
				errors.Add("Product On Sale already have a sale");
			return errors;
		}


		public void Delete(int id)
		{
			Purchase purchase = _purchaseRepository.Get(id);
			purchase.deleted = true;
			_purchaseRepository.Update(purchase);
		}

		public PaymentMethod CreatePaymentMethod(int paymentMethod, string description)
		{

			// I don't use case becouse id needs to be constant
			if (paymentMethod == PaymentMethod.CASH.id)
			{
				return _cashMethodService.Add(description);
			}
			else
			{
				if (paymentMethod == PaymentMethod.CARD.id)
				{
					return _cardMethodService.Add(description);
				}
			}
			return null;
		}


	}
}
