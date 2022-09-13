using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using marketplace.Helpers;
using Newtonsoft.Json.Linq;
using marketplace.Models;
using marketplace.DTO.UserDTO;
using MimeKit;
using System.Security.Authentication;
using marketplace.Helpers.Interfaces;
using marketplace.Services.Interfaces;

namespace marketplace.Controllers
{
    [ApiController]
    [Route("marketplace/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _config;

        private readonly IUserService _userService;
		private readonly IJwtMiddleware _jwtMiddleware;

		private readonly JWT _JWT;


        public LoginController(IOptions<JWT> JWT, IConfiguration config, IUserService userService,
			IJwtMiddleware jwtMiddleware)
        {
            _JWT = JWT.Value;
            _config = config;
            _userService = userService;
			_jwtMiddleware = jwtMiddleware;

		}

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Login([FromBody] LoginDTO login)
        {
            try
            {
                IActionResult response = Unauthorized();
                UserLoginDTO user = _userService.AuthenticateUser<UserLoginDTO.MapperProfile>(login);
                if (user != null)
                {
                    var tokenString = _jwtMiddleware.GenerateJWTToken(user,_JWT,_config);
                    user.token = tokenString;
                    response = Ok(
                        user
                    );
                }
                return response;
                // authentication successful so generate jwt token
            }
            catch (Exception e)
            {
                if (_config.GetSection("Environment")["Production"] == "true")
                    return StatusCode(500, "Server error, contact Technical Support");
                else
                    return StatusCode(500, "Internal server error." + e);
            }
        }


        [HttpPost("forgot-password")]
        [AllowAnonymous]
        public IActionResult ForgotPassword([FromBody] JObject __forgotP)
        {

            try
            {
                dynamic forgotP = __forgotP;
                string email = forgotP.email;
                User usuario = _userService.GetByEmail(email.ToString());
                if (usuario != null)
                {
                    Random random = new Random();
                    int num = random.Next();
                    usuario.password = num.ToString();
                    _userService.Update(usuario);

                    string html = "<p><font style='color:rgb(0, 0, 0)' size='4'>Reset password</font></p>";
                    html += string.Format("<p><font style='color:rgb(0, 0, 0)' size='3'>Email : {0}</font></p>", forgotP.email);
                    html += string.Format("<p><font style='color:rgb(0, 0, 0)' size='3'>Username : {0}</font></p>", usuario.username);
                    html += string.Format("<p><font style='color:rgb(0, 0, 0)' size='3'>New password : {0}</font></p>", num);

                    var message = new MimeMessage();
                    message.From.Add(new MailboxAddress("Juan Cruz Chebreli project", "yourmail"));
                    message.To.Add(new MailboxAddress("Juan Cruz Chebreli project", email.ToString()));
                    //elparadordeleste@hotmail.com
                    message.Subject = "Reset password";
                    var body = new TextPart("html")
                    {
                        Text = html
                    };

                    // now create the multipart/mixed container to hold the message text and the
                    // image attachment
                    var multipart = new Multipart("mixed");
                    multipart.Add(body);

                    // now set the multipart/mixed as the message body
                    message.Body = multipart;

                    using (var client = new MailKit.Net.Smtp.SmtpClient())
                    {
                        client.SslProtocols = SslProtocols.Tls | SslProtocols.Tls11 | SslProtocols.Tls12 | SslProtocols.Tls13;
                        client.CheckCertificateRevocation = false;
                        client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                        client.Connect("smtp.gmail.com", 587, false);
                        client.Authenticate("yourmail", "yourpassword");
                        client.Send(message);
                        client.Disconnect(true);
                    }
                    return Ok(new { message = "Message sent successfully" });
                }
                else
                {
                    return StatusCode(500, "User not found");
                };
            }
            catch (Exception e)
            {
                if (_config.GetSection("Environment")["Production"] == "true")
                    return StatusCode(500, "Server error, contact Technical Support");
                else
                    return StatusCode(500, "Internal server error." + e);
            }
        }
    }
}
