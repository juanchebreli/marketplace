using marketplace.DTO.ProductOnSaleDTO;
using marketplace.Models;
using marketplace.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace marketplace.Controllers
{
	[ApiController]
	[Authorize]
	[Route("marketplace/[controller]")]
	public class ProductOnSaleController : ControllerBase
	{
		private readonly IProductOnSaleService _productOnSaleService;
		private readonly IConfiguration _configuration;

		public ProductOnSaleController(IConfiguration configuration, IProductOnSaleService productOnSaleService)
		{
			_productOnSaleService = productOnSaleService;
			_configuration = configuration;
		}


		[HttpGet]
		public IActionResult All()
		{
			List<ProductOnSale> productOnSales = _productOnSaleService.GetAll();
			return Ok(productOnSales);
		}


		[HttpGet("{id}")]
		public IActionResult ById(int id)
		{
			ProductOnSale productOnSale = _productOnSaleService.Get(id);
			return Ok(productOnSale);
		}


		[HttpPost]
		public async Task<IActionResult> Crear([FromBody] ProductOnSaleCreateDTO entity)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest("Invalid data.");
			}

			 _productOnSaleService.Validate(0);

			ProductOnSale productOnSale = _productOnSaleService.Add(entity);
			if (productOnSale.offer)
			{
				await _productOnSaleService.SendNewOffer(productOnSale);
			}
			return Ok(productOnSale);
		}


		[Authorize(Roles = "Admin,Moderador")]
		[HttpPut]
		public IActionResult Editar([FromBody] ProductOnSaleUpdateDTO entity)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest("Invalid data.");
			}

			_productOnSaleService.Validate(entity.id);

			_productOnSaleService.Update(entity);
			return Ok();
		}

		[HttpDelete("{id}")]
		public IActionResult Delete(int id)
		{
			_productOnSaleService.Delete(id);
			return Ok();
		}
	}
}
