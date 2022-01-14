using marketplace.DTO.ProductDTO;
using marketplace.Models;
using marketplace.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace marketplace.Controllers
{
	[ApiController]
	[Authorize]
	[Route("marketplace/[controller]")]
	public class ProductController : ControllerBase
	{
		private readonly IProductService _productService;
		private readonly IConfiguration _configuration;

		public ProductController(IConfiguration configuration, IProductService productService)
		{
			_productService = productService;
			_configuration = configuration;
		}


		[HttpGet("all")]
		public IActionResult All()
		{
			try
			{
				List<Product> products = _productService.GetAll();
				return Ok(products);
			}
			catch (Exception e)
			{
				if (_configuration.GetSection("Environment")["Production"] == "true")
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
				Product product = _productService.Get(id);
				return Ok(product);
			}
			catch (Exception e)
			{
				if (_configuration.GetSection("Environment")["Production"] == "true")
					return StatusCode(500, "Server error, contact Technical Support");
				else
					return StatusCode(500, "Internal server error." + e);
			}
		}

		[HttpPost("create")]
		public IActionResult Crear([FromBody] ProductCreateDTO entity)
		{
			try
			{
				if (!ModelState.IsValid)
				{
					return BadRequest("Invalid data.");
				}
				List<string> errors = _productService.Validations(entity.name, 0);
				if (!errors.Any())
				{
					Product product = _productService.Add(entity);
					return Ok(product);
				}
				else
				{
					var errors_json = JsonConvert.SerializeObject(errors);
					return StatusCode(500, errors_json);
				}
			}
			catch (Exception e)
			{
				if (_configuration.GetSection("Environment")["Production"] == "true")
					return StatusCode(500, "Server error, contact Technical Support");
				else
					return StatusCode(500, "Internal server error." + e);
			}
		}


		[Authorize(Roles = "Admin,Moderador")]
		[HttpPut("edit")]
		public IActionResult Editar([FromBody] ProductUpdateDTO entity)
		{
			try
			{
				if (!ModelState.IsValid)
				{
					return BadRequest("Invalid data.");
				}

				List<string> errors = _productService.Validations(entity.name , entity.id);
				if (!errors.Any())
				{
					_productService.Update(entity);
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
				if (_configuration.GetSection("Environment")["Production"] == "true")
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
				_productService.Delete(id);
				return Ok();
			}
			catch (Exception e)
			{
				if (_configuration.GetSection("Environment")["Production"] == "true")
					return StatusCode(500, "Server error, contact Technical Support");
				else
					return StatusCode(500, "Internal server error." + e);
			}
		}
	}
}
