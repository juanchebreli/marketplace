using marketplace.DTO.UserDTO;
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
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;

        public UserController(IConfiguration configuration, IUserService userService)
        {
            _userService = userService;
            _configuration = configuration;
        }


        [HttpGet("all")]
        public IActionResult All()
        {
            try
            {
                List<User> users= _userService.GetAll();
                return Ok(users);
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
                User user = _userService.Get(id);
                return Ok(user);
            }
            catch (Exception e)
            {
                if (_configuration.GetSection("Environment")["Production"] == "true")
                    return StatusCode(500, "Server error, contact Technical Support");
                else
                    return StatusCode(500, "Internal server error." + e);
            }
        }

        [Authorize(Roles = "Admin,Mod")]
        [HttpPost("create")]
        public IActionResult Crear([FromBody] UserCreateDTO entity)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid data.");
                }
                List<string> errors = _userService.Validaciones(entity.email, entity.id, entity.username);
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
        public IActionResult Editar([FromBody] UserUpdateDTO entity)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid data.");
                }

                List<string> errors = _userService.Validaciones(entity.email, entity.id, entity.username);
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
                 _userService.Delete(id);
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
