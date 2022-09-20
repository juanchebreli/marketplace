﻿using marketplace.DTO.PaymentMethodDTO;
using marketplace.DTO.PurchaseDTO;
using marketplace.Models;

namespace marketplace.Services.Interfaces
{
	public interface IPurchaseService
	{
		List<Purchase> GetAll();
		Purchase Get(int id);
		Purchase Add(PurchaseCreateDTO entity);
		Purchase Update(PurchaseUpdateDTO entity);
		Purchase Update(Purchase entity);
		void Validate(int Userid, int ProductOnSaleid, string paymentMethod, int id);
		void Delete(int id);
	}
}
