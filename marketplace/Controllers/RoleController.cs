using marketplace.DTO.RoleDTO;
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
	public class RoleController : ControllerBase
	{
		private readonly IRoleService _roleService;
		private readonly IConfiguration _configuration;

		public RoleController(IConfiguration configuration, IRoleService roleService)
		{
			_roleService = roleService;
			_configuration = configuration;
		}


		[HttpGet("all")]
		public IActionResult All()
		{
			try
			{
				List<Role> roles = _roleService.GetAll();
				return Ok(roles);
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
				Role role = _roleService.Get(id);
				return Ok(role);
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
		public IActionResult Crear([FromBody] RoleCreateDTO entity)
		{
			try
			{
				if (!ModelState.IsValid)
				{
					return BadRequest("Invalid data.");
				}
				List<string> errors = _roleService.Validations(entity.name, 0);
				if (!errors.Any())
				{
					Role role = _roleService.Add(entity);
					return Ok(role);
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
		public IActionResult Editar([FromBody] RoleUpdateDTO entity)
		{
			try
			{
				if (!ModelState.IsValid)
				{
					return BadRequest("Invalid data.");
				}

				List<string> errors = _roleService.Validations(entity.name, entity.id);
				if (!errors.Any())
				{
					_roleService.Update(entity);
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
				_roleService.Delete(id);
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
