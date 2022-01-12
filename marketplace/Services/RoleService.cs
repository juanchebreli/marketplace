using marketplace.MappingConfiguration;
using AutoMapper;
using marketplace.DTO.RoleDTO;
using marketplace.Helpers;
using marketplace.Models;
using marketplace.Repositories;

namespace marketplace.Services
{
	public interface IRoleService
	{
		List<Role> GetAll();
		Role Get(int id);
		Role Add(RoleCreateDTO entity);
		Role Update(RoleUpdateDTO entity);
		Role Update(Role entity);
		List<string> Validations(string roleName, int id);
		void Delete(int id);
	}

	public class RoleService : IRoleService
	{
		private readonly IRoleRepository _roleRepository;
		private readonly IConfiguration _configuration;

		public RoleService(IRoleRepository roleRepository, IConfiguration configuration)
		{

			_roleRepository = roleRepository;
			_configuration = configuration;

		}

		public List<Role> GetAll()
		{
			return _roleRepository.GetAll();
		}

		public Role Get(int id)
		{
			return _roleRepository.Get(id);
		}

		public Role Add(RoleCreateDTO entity)
		{
			return _roleRepository.Add<RoleCreateDTO, RoleCreateDTO.MapperProfile>(entity);
		}

		public Role Update(RoleUpdateDTO entity)
		{
			return _roleRepository.Update<RoleUpdateDTO, RoleUpdateDTO.MapperProfile>(entity);
		}

		public Role Update(Role entity)
		{
			return _roleRepository.Update(entity);
		}
		public List<string> Validations(string roletname, int id)
		{
			List<string> errors = new List<string>();
			if (!_roleRepository.FreeName(roletname, id))
				errors.Add("That name is already being used by another role");
			return errors;
		}


		public void Delete(int id)
		{
			Role role = _roleRepository.Get(id);
			role.deleted = true;
			_roleRepository.Update(role);
		}
	}
}
