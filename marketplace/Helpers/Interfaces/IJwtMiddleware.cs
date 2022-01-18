using marketplace.DTO.UserDTO;

namespace marketplace.Helpers.Interfaces
{
	public interface IJwtMiddleware
	{
		public string GenerateJWTToken(UserLoginDTO userInfo, JWT JWT, IConfiguration config);
	}
}
