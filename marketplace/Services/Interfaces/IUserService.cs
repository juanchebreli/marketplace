using AutoMapper;
using marketplace.DTO.UserDTO;
using marketplace.Models;

namespace marketplace.Services.Interfaces
{
	public interface IUserService
	{
		UserLoginDTO AuthenticateUser<TMapperProfile>(LoginDTO loginCredentials) where TMapperProfile : Profile, new();
		List<User> GetAll();
		User Get(int id);
		User Add(UserCreateDTO entity);
		User Update(UserUpdateDTO entity);
		User Update(User entity);
		void Validate(string email, int id, string username);
		User GetByEmail(string email);
		void Delete(int id);
	}
}
