using marketplace.MappingConfiguration;
using AutoMapper;
using marketplace.DTO.ProductDTO;
using marketplace.Helpers;
using marketplace.Models;
using marketplace.Repositories;

namespace marketplace.Services
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

	public class ProductService : IProductService
	{
		private readonly IProductRepository _productRepository;
		private readonly IConfiguration _configuration;

		public ProductService(IProductRepository productRepository, IConfiguration configuration)
		{

			_productRepository = productRepository;
			_configuration = configuration;

		}

		public List<Product> GetAll()
		{
			return _productRepository.GetAll();
		}

		public Product Get(int id)
		{
			return _productRepository.Get(id);
		}

		public Product Add(ProductCreateDTO entity)
		{
			entity.deleted = false;
			return _productRepository.Add<ProductCreateDTO, ProductCreateDTO.MapperProfile>(entity);
		}

		public Product Update(ProductUpdateDTO entity)
		{
			return _productRepository.Update<ProductUpdateDTO, ProductUpdateDTO.MapperProfile>(entity);
		}

		public Product Update(Product entity)
		{
			return _productRepository.Update(entity);
		}
		public List<string> Validations(string productname, int id)
		{
			List<string> errors = new List<string>();
			if (!_productRepository.FreeName(productname, id))
				errors.Add("That name is already being used by another product");
			return errors;
		}


		public void Delete(int id)
		{
			Product product = _productRepository.Get(id);
			product.deleted = true;
			_productRepository.Update(product);
		}
	}
}
