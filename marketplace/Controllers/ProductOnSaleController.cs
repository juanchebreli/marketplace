using marketplace.DTO.ProductOnSaleDTO;
using marketplace.Models;
using marketplace.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace marketplace.Controllers
{
	[ApiController]
	[Authorize]
	[Route("api/[controller]")]
	public class ProductOnSaleController : ControllerBase
	{
		private readonly IProductOnSaleService _productOnSaleService;
		private readonly IConfiguration _configuration;

		public ProductOnSaleController(IConfiguration configuration, IProductOnSaleService productOnSaleService)
		{
			_productOnSaleService = productOnSaleService;
			_configuration = configuration;
		}


		[HttpGet("all")]
		public IActionResult All()
		{
			try
			{
				List<ProductOnSale> productOnSales = _productOnSaleService.GetAll();
				return Ok(productOnSales);
			}
			catch (Exception e)
			{
				if (_configuration.GetSection("Environment")["ProductOnSaleion"] == "true")
					return StatusCode(500, "Server error, contact Technical Support");
				else
					return StatusCode(500, "Internal server error." + e);
			}
		}


		[HttpGet("{id}")]
		public IActionResult ById(int id)
		{
			try
			{
				ProductOnSale productOnSale = _productOnSaleService.Get(id);
				return Ok(productOnSale);
			}
			catch (Exception e)
			{
				if (_configuration.GetSection("Environment")["ProductOnSaleion"] == "true")
					return StatusCode(500, "Server error, contact Technical Support");
				else
					return StatusCode(500, "Internal server error." + e);
			}
		}

		[Authorize(Roles = "Admin,Mod")]
		[HttpPost("create")]
		public IActionResult Crear([FromBody] ProductOnSaleCreateDTO entity)
		{
			try
			{
				if (!ModelState.IsValid)
				{
					return BadRequest("Invalid data.");
				}
				List<string> errors = _productOnSaleService.Validations(0);
				if (!errors.Any())
				{
					ProductOnSale productOnSale = _productOnSaleService.Add(entity);
					return Ok(productOnSale);
				}
				else
				{
					var errors_json = JsonConvert.SerializeObject(errors);
					return StatusCode(500, errors_json);
				}
			}
			catch (Exception e)
			{
				if (_configuration.GetSection("Environment")["ProductOnSaleion"] == "true")
					return StatusCode(500, "Server error, contact Technical Support");
				else
					return StatusCode(500, "Internal server error." + e);
			}
		}


		[Authorize(Roles = "Admin,Moderador")]
		[HttpPut("edit")]
		public IActionResult Editar([FromBody] ProductOnSaleUpdateDTO entity)
		{
			try
			{
				if (!ModelState.IsValid)
				{
					return BadRequest("Invalid data.");
				}

				List<string> errors = _productOnSaleService.Validations(entity.id);
				if (!errors.Any())
				{
					_productOnSaleService.Update(entity);
					return Ok();
				}
				else
				{
					var errors_json = JsonConvert.SerializeObject(errors);
					return StatusCode(500, errors_json);
				}
			}
			catch (Exception e)
			{
				if (_configuration.GetSection("Environment")["ProductOnSaleion"] == "true")
					return StatusCode(500, "Server error, contact Technical Support");
				else
					return StatusCode(500, "Internal server error." + e);
			}
		}

		[HttpDelete("{id}")]
		public IActionResult Delete(int id)
		{
			try
			{
				_productOnSaleService.Delete(id);
				return Ok();
			}
			catch (Exception e)
			{
				if (_configuration.GetSection("Environment")["ProductOnSaleion"] == "true")
					return StatusCode(500, "Server error, contact Technical Support");
				else
					return StatusCode(500, "Internal server error." + e);
			}
		}
	}
}
