using marketplace.DTO.UserDTO;
using marketplace.Helpers.Jwt;

namespace marketplace.Helpers.Interfaces
{
    public interface IJwtMiddleware
	{
		public string GenerateJWTToken(UserLoginDTO userInfo, JWT JWT, IConfiguration config);
	}
}
