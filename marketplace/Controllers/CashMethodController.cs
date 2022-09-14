using marketplace.DTO.PaymentMethodDTO.CashMethodDTO;
using marketplace.Models;
using marketplace.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace marketplace.Controllers
{
	[ApiController]
	[Authorize]
	[Route("marketplace/[controller]")]
	public class CashMethodController : ControllerBase
	{
		private readonly ICashMethodService _CashMethodService;
		private readonly IConfiguration _configuration;

		public CashMethodController(IConfiguration configuration, ICashMethodService CashMethodService)
		{
			_CashMethodService = CashMethodService;
			_configuration = configuration;
		}


		[AllowAnonymous]
		[HttpGet]
		public IActionResult All()
		{
			List<PaymentMethod> CashMethods = _CashMethodService.GetAll();
			return Ok(CashMethods);
		}


		[HttpGet("{id}")]
		public IActionResult ById(int id)
		{
			PaymentMethod CashMethod = _CashMethodService.Get(id);
			return Ok(CashMethod);
		}



		[HttpPut]
		public IActionResult Editar([FromBody] CashMethodUpdateDTO entity)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest("Invalid data.");
			}

			_CashMethodService.Update(entity);
			return Ok();
		}

		[HttpDelete("{id}")]
		public IActionResult Delete(int id)
		{

			_CashMethodService.Delete(id);
			return Ok();
		}
	}
}
