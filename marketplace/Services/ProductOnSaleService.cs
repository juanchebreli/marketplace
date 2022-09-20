using marketplace.MappingConfiguration;
using marketplace.DTO.ProductOnSaleDTO;
using marketplace.Models;
using Microsoft.AspNetCore.SignalR;
using marketplace.WebSocket;
using marketplace.Services.Interfaces;
using marketplace.Repositories.Interfaces;
using Newtonsoft.Json;
using marketplace.Helpers.Exceptions.Implements;

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
			return _productOnSaleRepository.GetAll();
		}

		public ProductOnSale Get(int id)
		{
			return _productOnSaleRepository.Get(id);
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
