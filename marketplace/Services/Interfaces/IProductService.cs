using marketplace.DTO.ProductDTO;
using marketplace.Models;

namespace marketplace.Services.Interfaces
{
	public interface IProductService
	{
		List<Product> GetAll();
		Product Get(int id);
		Product Add(ProductCreateDTO entity);
		Product Update(ProductUpdateDTO entity);
		Product Update(Product entity);
		List<string> Validations(string productName, int id);
		void Delete(int id);
	}
}
