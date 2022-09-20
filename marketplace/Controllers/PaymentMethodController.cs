using marketplace.DTO.PaymentMethodDTO.CardMethodDTO;
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
	public class PaymentMethodController : ControllerBase
	{
		private readonly IPaymentService _paymentService;
		private readonly IConfiguration _configuration;

		public PaymentMethodController(IConfiguration configuration, IPaymentService paymentService)
		{
			_paymentService = paymentService;
			_configuration = configuration;
		}


		[AllowAnonymous]
		[HttpGet]
		public IActionResult All()
		{
			List<PaymentMethod> paymentMethods = _paymentService.GetAll();
			return Ok(paymentMethods);
		}

		[HttpGet("cash")]
		public IActionResult AllCash()
		{
			List<CashMethod> cashMethods = _paymentService.GetAllCash();
			return Ok(cashMethods);
		}

		[HttpGet("card")]
		public IActionResult AllCard()
		{
			List<CardMethod> cardMethods = _paymentService.GetAllCard();
			return Ok(cardMethods);
		}


		[HttpGet("{id}")]
		public IActionResult ById(int id)
		{
			PaymentMethod paymentMethods = _paymentService.Get(id);
			return Ok(paymentMethods);
		}



		[HttpPut("cash")]
		public IActionResult Editar([FromBody] CashMethodUpdateDTO entity)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest("Invalid data.");
			}

			_paymentService.Update(entity);
			return Ok();
		}

		[HttpPut("card")]
		public IActionResult Editar([FromBody] CardMethodUpdateDTO entity)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest("Invalid data.");
			}

			_paymentService.Update(entity);
			return Ok();
		}

		[HttpDelete("{id}")]
		public IActionResult Delete(int id)
		{

			_paymentService.Delete(id);
			return Ok();
		}
	}
}
