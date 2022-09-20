using marketplace.DTO.ProductDTO;
using marketplace.Models;
using marketplace.Services.Interfaces;
using marketplace.Repositories.Interfaces;
using Newtonsoft.Json;
using marketplace.Helpers.Exceptions.Implements;
using System.Text;

namespace marketplace.Services
{
	

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
			List<Product> products =_productRepository.GetAll();
			if (products.Count == 0) throw new NoContentException("Don't have a products");
			return products;
		}

		public Product Get(int id)
		{
			Product product = _productRepository.Get(id);
			if (product == null) throw new NotFoundException(new StringBuilder("Not found a product with id: {0}", id).ToString());
			return product;
		}

		public Product Add(ProductCreateDTO entity)
		{
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
		public void Validate(string productname, int id)
		{
			List<string> errors = new List<string>();
			if (!this.FreeName(productname, id))
				errors.Add("That name is already being used by another product");

			if (errors.Any())
			{
				string errosJson = JsonConvert.SerializeObject(errors);
				throw new BadRequestException(errosJson);
			}
		}


		public void Delete(int id)
		{
			Product product = _productRepository.Get(id);
			if (product == null) throw new NotFoundException(new StringBuilder("Not found a product with id: {0}", id).ToString());

			product.deleted = true;
			_productRepository.Update(product);
		}

		#region private
		public bool FreeName(string name, int id)
		{
			Product entity = _productRepository.GetByName(name);

			if (entity != null && entity.id == id)
				return true;
			else
				return (entity == null);
		}
		#endregion
	}
}
