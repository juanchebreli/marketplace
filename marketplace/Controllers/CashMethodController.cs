using marketplace.DTO.CashMethodDTO;
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
		[HttpGet("all")]
		public IActionResult All()
		{
			try
			{
				List<PaymentMethod> CashMethods = _CashMethodService.GetAll();
				return Ok(CashMethods);
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
				PaymentMethod CashMethod = _CashMethodService.Get(id);
				return Ok(CashMethod);
			}
			catch (Exception e)
			{
				if (_configuration.GetSection("Environment")["Production"] == "true")
					return StatusCode(500, "Server error, contact Technical Support");
				else
					return StatusCode(500, "Internal server error." + e);
			}
		}

		[AllowAnonymous]
		[Authorize(Roles = "Admin,Mod")]
		[HttpPost("create")]
		public IActionResult Crear([FromBody] CashMethodCreateDTO entity)
		{
			try
			{
				if (!ModelState.IsValid)
				{
					return BadRequest("Invalid data.");
				}
				CashMethod CashMethod = _CashMethodService.Add(entity);
				return Ok(CashMethod);
			}
			catch (Exception e)
			{
				if (_configuration.GetSection("Environment")["Production"] == "true")
					return StatusCode(500, "Server error, contact Technical Support");
				else
					return StatusCode(500, "Internal server error." + e);
			}
		}



		[HttpPut("edit")]
		public IActionResult Editar([FromBody] CashMethodUpdateDTO entity)
		{
			try
			{
				if (!ModelState.IsValid)
				{
					return BadRequest("Invalid data.");
				}

				_CashMethodService.Update(entity);
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

		[HttpDelete("{id}")]
		public IActionResult Delete(int id)
		{
			try
			{
				_CashMethodService.Delete(id);
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
