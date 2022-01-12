using marketplace.DTO.PurchaseDTO;
using marketplace.Models;
using marketplace.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace marketplace.Controllers
{
	[ApiController]
	//[Authorize]
	[Route("marketplace/[controller]")]
	public class PurchaseController : ControllerBase
	{
		private readonly IPurchaseService _purchaseService;
		private readonly IConfiguration _configuration;

		public PurchaseController(IConfiguration configuration, IPurchaseService purchaseService)
		{
			_purchaseService = purchaseService;
			_configuration = configuration;
		}


		[HttpGet("all")]
		public IActionResult All()
		{
			try
			{
				List<Purchase> purchases = _purchaseService.GetAll();
				return Ok(purchases);
			}
			catch (Exception e)
			{
				if (_configuration.GetSection("Environment")["Purchaseion"] == "true")
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
				Purchase purchase = _purchaseService.Get(id);
				return Ok(purchase);
			}
			catch (Exception e)
			{
				if (_configuration.GetSection("Environment")["Purchaseion"] == "true")
					return StatusCode(500, "Server error, contact Technical Support");
				else
					return StatusCode(500, "Internal server error." + e);
			}
		}

		[HttpPost("create")]
		public IActionResult Crear([FromBody] PurchaseCreateDTO entity)
		{
			try
			{
				if (!ModelState.IsValid)
				{
					return BadRequest("Invalid data.");
				}
				List<string> errors = _purchaseService.Validations(entity.Userid,entity.ProductOnSaleid, 0);
				if (!errors.Any())
				{
					Purchase purchase = _purchaseService.Add(entity);
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
				if (_configuration.GetSection("Environment")["Purchaseion"] == "true")
					return StatusCode(500, "Server error, contact Technical Support");
				else
					return StatusCode(500, "Internal server error." + e);
			}
		}


		[Authorize(Roles = "Admin,Moderador")]
		[HttpPut("edit")]
		public IActionResult Editar([FromBody] PurchaseUpdateDTO entity)
		{
			try
			{
				if (!ModelState.IsValid)
				{
					return BadRequest("Invalid data.");
				}

				List<string> errors = _purchaseService.Validations(entity.Userid,entity.ProductOnSaleid, entity.id);
				if (!errors.Any())
				{
					_purchaseService.Update(entity);
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
				if (_configuration.GetSection("Environment")["Purchaseion"] == "true")
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
				_purchaseService.Delete(id);
				return Ok();
			}
			catch (Exception e)
			{
				if (_configuration.GetSection("Environment")["Purchaseion"] == "true")
					return StatusCode(500, "Server error, contact Technical Support");
				else
					return StatusCode(500, "Internal server error." + e);
			}
		}
	}
}
