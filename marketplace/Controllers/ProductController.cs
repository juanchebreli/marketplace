using marketplace.DTO.ProductDTO;
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
	public class ProductController : ControllerBase
	{
		private readonly IProductService _productService;
		private readonly IConfiguration _configuration;

		public ProductController(IConfiguration configuration, IProductService productService)
		{
			_productService = productService;
			_configuration = configuration;
		}


		[HttpGet]
		public IActionResult All()
		{
			List<Product> products = _productService.GetAll();
			return Ok(products);
		}


		[HttpGet("{id}")]
		public IActionResult ById(int id)
		{
			Product product = _productService.Get(id);
			return Ok(product);
		}

		[HttpPost]
		public IActionResult Crear([FromBody] ProductCreateDTO entity)
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


		[Authorize(Roles = "Admin,Moderador")]
		[HttpPut("edit")]
		public IActionResult Editar([FromBody] ProductUpdateDTO entity)
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

		[HttpDelete("{id}")]
		public IActionResult Delete(int id)
		{
			_productService.Delete(id);
			return Ok();
		}
	}
}
