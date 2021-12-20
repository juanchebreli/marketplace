using API.MappingConfiguration;
using AutoMapper;
using marketplace.DTO.UserDTO;
using marketplace.Models;
using marketplace.Repositories;

namespace marketplace.Services
{
    public interface IUserService
    {
		UserLoginDTO AuthenticateUser<TMapperProfile>(LoginDTO loginCredentials) where TMapperProfile : Profile, new();
		IEnumerable<User> GetAll();
		User Get(int id);
		User Add(User entity);
		User Update(User entity);
		List<string> Validaciones(string email, int id, string username, string dni);
		User GetByEmail(string email);
	}

	public class UserService : IUserService
	{
		private readonly IUserRepository _userRepository;

		public UserService(IUserRepository userRepository)
		{

			_userRepository = userRepository;

		}

		public UserLoginDTO AuthenticateUser<TMapperProfile>(LoginDTO loginCredentials) where TMapperProfile : Profile, new()
		{
			return Mapeador.Map<User, UserLoginDTO, TMapperProfile>(_userRepository.AuthenticateUser(loginCredentials));
        }

		public IEnumerable<User> GetAll()
		{
			return _userRepository.All();
		}

		public User Get(int id)
		{
			return _userRepository.Get(id);
		}

		public User Add(User entity)
		{
			return _userRepository.Add(entity);
		}

		public User Update(User entity)
		{
			return _userRepository.Update(entity);
		}

		public List<string> Validaciones(string email, int id, string username, string dni)
		{
			List<string> errors = new List<string>();
			if (!_userRepository.FreeEmail(email, id))
				errors.Add("Ese email ya esta utilizado por otro usuario");
			if (!_userRepository.FreeUsername(username, id))
				errors.Add("Ese nombre de usuario ya esta utilizado por otro usuario");
			return errors;
		}

		public User GetByEmail(string email)
        {
			return _userRepository.GetByEmail(email);
        }
	}
}
