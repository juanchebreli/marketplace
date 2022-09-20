﻿using marketplace.MappingConfiguration;
using marketplace.DTO.PurchaseDTO;
using marketplace.Models;
using Microsoft.EntityFrameworkCore.Storage;
using marketplace.Context;
using marketplace.Services.Interfaces;
using marketplace.Repositories.Interfaces;
using Newtonsoft.Json;
using marketplace.Helpers.Exceptions.Implements;
using marketplace.Helpers.Enums;
using marketplace.Helpers.Factory;

namespace marketplace.Services
{
	public class PurchaseService : IPurchaseService
	{
		private readonly IPurchaseRepository _purchaseRepository;
		private readonly IConfiguration _configuration;
		private readonly IUserService _userService;
		private readonly IProductOnSaleService _productOnSaleserService;
		private readonly PaymentMethodFactory _paymentMethodFactory;
		private readonly AppDbContext AppDbContext;


		public PurchaseService(IPurchaseRepository purchaseRepository, IConfiguration configuration, IUserService userService,
			IProductOnSaleService productOnSaleserService, PaymentMethodFactory paymentMethodFactory, AppDbContext dbContext)
		{

			_purchaseRepository = purchaseRepository;
			_configuration = configuration;
			_userService = userService;
			_productOnSaleserService = productOnSaleserService;
			_paymentMethodFactory = paymentMethodFactory;
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
					PaymentMethod paymentMethod = _paymentMethodFactory.CreatePaymentMethod(entity.paymentMethod);
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
		public void Validate(int Userid, int ProductOnSaleid, string paymentMethod, int id)
		{
			List<string> errors = new List<string>();
			if (_userService.Get(Userid) == null)
				errors.Add("User dont' exist");
			if (_productOnSaleserService.Get(ProductOnSaleid) == null)
				errors.Add("Product On Sale dont' exist");
			if (_purchaseRepository.GetByProductOnSale(ProductOnSaleid) != null)
				errors.Add("Product On Sale already have a sale");
			if ((!PaymentMethodsEnum.Cash.ToString().Equals(paymentMethod)) && (!PaymentMethodsEnum.Card.ToString().Equals(paymentMethod)))
				errors.Add("Payment method is invalid");

			if (errors.Any())
			{
				string errosJson = JsonConvert.SerializeObject(errors);
				throw new BadRequestException(errosJson);
			}
		}


		public void Delete(int id)
		{
			Purchase purchase = _purchaseRepository.Get(id);
			purchase.deleted = true;
			_purchaseRepository.Update(purchase);
		}

	}
}
