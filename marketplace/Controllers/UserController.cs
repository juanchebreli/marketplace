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

            _userService.Validate(entity.email, 0, entity.username);

            User user = _userService.Add(entity);
            return Ok(user);
        }


        [Authorize(Roles = "Admin,Moderador")]
        [HttpPut]
        public IActionResult Editar([FromBody] UserUpdateDTO entity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data.");
            }

            _userService.Validate(entity.email, entity.id, entity.username);

            _userService.Update(entity);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _userService.Delete(id);
            return Ok();
        }
    }
}
