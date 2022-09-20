using marketplace.DTO.PurchaseDTO;
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
	public class PurchaseController : ControllerBase
	{
		private readonly IPurchaseService _purchaseService;
		private readonly IConfiguration _configuration;

		public PurchaseController(IConfiguration configuration, IPurchaseService purchaseService)
		{
			_purchaseService = purchaseService;
			_configuration = configuration;
		}


		[HttpGet]
		public IActionResult All()
		{
			List<Purchase> purchases = _purchaseService.GetAll();
			return Ok(purchases);
		}


		[HttpGet("{id}")]
		public IActionResult ById(int id)
		{
			Purchase purchase = _purchaseService.Get(id);
			return Ok(purchase);
		}

		[HttpPost]
		public IActionResult Crear([FromBody] PurchaseCreateDTO entity)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest("Invalid data.");
			}

			_purchaseService.Validate(entity.Userid,entity.ProductOnSaleid,entity.paymentMethod.method, 0);

			Purchase purchase = _purchaseService.Add(entity);
			return Ok();
		}


		[Authorize(Roles = "Admin,Moderador")]
		[HttpPut]
		public IActionResult Editar([FromBody] PurchaseUpdateDTO entity)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest("Invalid data.");
			}

			_purchaseService.Validate(entity.Userid,entity.ProductOnSaleid,entity.PaymentMethod, entity.id);

			_purchaseService.Update(entity);
			return Ok();
		}

		[HttpDelete("{id}")]
		public IActionResult Delete(int id)
		{
			_purchaseService.Delete(id);
			return Ok();
		}
	}
}
