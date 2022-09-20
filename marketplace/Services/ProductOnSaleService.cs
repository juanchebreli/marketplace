using marketplace.MappingConfiguration;
using marketplace.DTO.ProductOnSaleDTO;
using marketplace.Models;
using Microsoft.AspNetCore.SignalR;
using marketplace.WebSocket;
using marketplace.Services.Interfaces;
using marketplace.Repositories.Interfaces;
using Newtonsoft.Json;
using marketplace.Helpers.Exceptions.Implements;
using System.Text;

namespace marketplace.Services
{
	public class ProductOnSaleService : IProductOnSaleService
	{
		private readonly IProductOnSaleRepository _productOnSaleRepository;
		private readonly IConfiguration _configuration;
		private readonly IHubContext<NewOfferHub> _newOfferHub;

		public ProductOnSaleService(IProductOnSaleRepository productOnSaleRepository, IConfiguration configuration, IHubContext<NewOfferHub> newOfferHub)
		{

			_productOnSaleRepository = productOnSaleRepository;
			_configuration = configuration;
			_newOfferHub = newOfferHub;

		}

		public List<ProductOnSale> GetAll()
		{
			List<ProductOnSale> productOnSales =  _productOnSaleRepository.GetAll();
			if (productOnSales.Count == 0) throw new NoContentException("Don't have a product on sale");
			return productOnSales;
		}

		public ProductOnSale Get(int id)
		{
			ProductOnSale productOnSale= _productOnSaleRepository.Get(id);
			if (productOnSale == null) throw new NotFoundException(new StringBuilder("Not found a product on sale with id: {0}", id).ToString());
			return productOnSale;
		}

		public ProductOnSale Add(ProductOnSaleCreateDTO entity)
		{
			return _productOnSaleRepository.Add<ProductOnSaleCreateDTO, ProductOnSaleCreateDTO.MapperProfile>(entity);
		}

		public ProductOnSale Update(ProductOnSaleUpdateDTO entity)
		{
			return _productOnSaleRepository.Update<ProductOnSaleUpdateDTO, ProductOnSaleUpdateDTO.MapperProfile>(entity);
		}

		public ProductOnSale Update(ProductOnSale entity)
		{
			return _productOnSaleRepository.Update(entity);
		}
		public void Validate(int id)
		{
			List<string> errors = new List<string>();
			if (errors.Any())
			{
				string errosJson = JsonConvert.SerializeObject(errors);
				throw new BadRequestException(errosJson);
			}
		}


		public void Delete(int id)
		{
			ProductOnSale productOnSale = _productOnSaleRepository.Get(id);
			if (productOnSale == null) throw new NotFoundException(new StringBuilder("Not found a product on sale with id: {0}", id).ToString());

			productOnSale.deleted = true;
			_productOnSaleRepository.Update(productOnSale);
		}

		public async Task SendNewOffer(ProductOnSale entity)
		{
			ProductOnSaleOfferDTO productOnSaleOffer = CustomMapper.Map<ProductOnSale, ProductOnSaleOfferDTO, ProductOnSaleOfferDTO.MapperProfile>(entity);
			await NewOfferHub.SendNewOffer(_newOfferHub, productOnSaleOffer);
		}
	}
}
