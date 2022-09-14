using marketplace.DTO.UserDTO;
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
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;

        public UserController(IConfiguration configuration, IUserService userService)
        {
            _userService = userService;
            _configuration = configuration;
        }


        [HttpGet]
        public IActionResult All()
        {
            List<User> users= _userService.GetAll();
            return Ok(users);
        }


        [HttpGet("{id}")]
        public IActionResult ById(int id)
        {
            User user = _userService.Get(id);
            return Ok(user);
        }

		
        [HttpPost]
        public IActionResult Crear([FromBody] UserCreateDTO entity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data.");
            }

            List<string> errors = _userService.Validations(entity.email, 0, entity.username);
            if (!errors.Any())
            {
                User user = _userService.Add(entity);
                return Ok(user);
            }
            else
            {
                var errors_json = JsonConvert.SerializeObject(errors);
                return StatusCode(500, errors_json);
            }
        }


        [Authorize(Roles = "Admin,Moderador")]
        [HttpPut]
        public IActionResult Editar([FromBody] UserUpdateDTO entity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data.");
            }

            List<string> errors = _userService.Validations(entity.email, entity.id, entity.username);
            if (!errors.Any())
            {
                _userService.Update(entity);
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
            _userService.Delete(id);
            return Ok();
        }
    }
}
