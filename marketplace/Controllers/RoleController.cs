using marketplace.DTO.RoleDTO;
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
	public class RoleController : ControllerBase
	{
		private readonly IRoleService _roleService;
		private readonly IConfiguration _configuration;

		public RoleController(IConfiguration configuration, IRoleService roleService)
		{
			_roleService = roleService;
			_configuration = configuration;
		}


		[HttpGet]
		public IActionResult All()
		{
			List<Role> roles = _roleService.GetAll();
			return Ok(roles);
		}


		[HttpGet("{id}")]
		public IActionResult ById(int id)
		{
			Role role = _roleService.Get(id);
			return Ok(role);
		}

		[HttpPost]
		public IActionResult Crear([FromBody] RoleCreateDTO entity)
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


		[Authorize(Roles = "Admin,Moderador")]
		[HttpPut]
		public IActionResult Editar([FromBody] RoleUpdateDTO entity)
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

		[HttpDelete("{id}")]
		public IActionResult Delete(int id)
		{
			_roleService.Delete(id);
			return Ok();
		}

	}
}
