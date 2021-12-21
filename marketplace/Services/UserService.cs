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
		List<User> GetAll();
		User Get(int id);
		User Add(UserCreateDTO entity);
		User Update(UserUpdateDTO entity);
		User Update(User entity);
		List<string> Validaciones(string email, int id, string username);
		User GetByEmail(string email);
		void Delete(int id);
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

		public List<User> GetAll()
		{
			return _userRepository.GetAll();
		}

		public User Get(int id)
		{
			return _userRepository.Get(id);
		}

		public User Add(UserCreateDTO entity)
		{
			entity.deleted = false;
			return _userRepository.Add<UserCreateDTO, UserCreateDTO.MapperProfile>(entity);
		}

		public User Update(UserUpdateDTO entity)
		{
			return _userRepository.Update<UserUpdateDTO, UserUpdateDTO.MapperProfile>(entity);
		}

		public User Update(User entity)
		{
			return _userRepository.Update(entity);
		}
		public List<string> Validaciones(string email, int id, string username)
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

		public void Delete(int id)
        {
			User user = _userRepository.Get(id);
			user.deleted = true;
			_userRepository.Update(user);
        }
	}
}
