using marketplace.DTO.ProductOnSaleDTO;
using marketplace.Models;

namespace marketplace.Services.Interfaces
{
	public interface IProductOnSaleService
	{
		List<ProductOnSale> GetAll();
		ProductOnSale Get(int id);
		ProductOnSale Add(ProductOnSaleCreateDTO entity);
		ProductOnSale Update(ProductOnSaleUpdateDTO entity);
		ProductOnSale Update(ProductOnSale entity);
		void Validate(int id);
		void Delete(int id);
		Task SendNewOffer(ProductOnSale entity);
	}
}
