using marketplace.MappingConfiguration;
using AutoMapper;
using marketplace.DTO.UserDTO;
using marketplace.Models;
using marketplace.Helpers.Interfaces;
using marketplace.Repositories.Interfaces;
using marketplace.Services.Interfaces;
using Newtonsoft.Json;
using marketplace.Helpers.Exceptions.Implements;

namespace marketplace.Services
{
	public class UserService : IUserService
	{
		private readonly IUserRepository _userRepository;
		private readonly IConfiguration _configuration;
		private readonly ICryptoEngine _cryptoEngine;

		public UserService(IUserRepository userRepository, IConfiguration configuration, ICryptoEngine cryptoEngine)
		{

			_userRepository = userRepository;
			_configuration = configuration;
			_cryptoEngine = cryptoEngine;

		}

		public UserLoginDTO AuthenticateUser<TMapperProfile>(LoginDTO loginCredentials) where TMapperProfile : Profile, new()
		{
			User user = _userRepository.AuthenticateUser(loginCredentials);
			if( user != null)
			{
				string key = _configuration.GetSection("Encrypt")["Key"];
				string passwordDecrypt = _cryptoEngine.Decrypt(user.password, key);
				if (passwordDecrypt == loginCredentials.password)
				{
					return CustomMapper.Map<User, UserLoginDTO, TMapperProfile>(user);
				}
			}
			return null;
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
			string key = _configuration.GetSection("Encrypt")["Key"];
			entity.password = _cryptoEngine.Encrypt(entity.password, key);
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
		public void Validate(string email, int id, string username)
		{
			List<string> errors = new List<string>();
			if (!this.FreeEmail(email, id))
				errors.Add("That email is already being used by another user");
			if (!this.FreeUsername(username, id))
				errors.Add("That username is already being used by another user");

			if (errors.Any())
			{
				string errosJson = JsonConvert.SerializeObject(errors);
				throw new BadRequestException(errosJson);
			}
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

		#region private
		public bool FreeEmail(string name, int id)
		{
			User entity = _userRepository.GetByEmail(name);

			if (entity != null && entity.id == id)
				return true;
			else
				return (entity == null);
		}

		public bool FreeUsername(string username, int id)
		{
			User entity = _userRepository.GetByName(username);

			if (entity != null && entity.id == id)
				return true;
			else
				return (entity == null);
		}
		#endregion
	}
}
