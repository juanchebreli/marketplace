using marketplace.DTO.RoleDTO;
using marketplace.Models;
using marketplace.Services.Interfaces;
using marketplace.Repositories.Interfaces;
using Newtonsoft.Json;
using marketplace.Helpers.Exceptions.Implements;

namespace marketplace.Services
{
	

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
		public void Validate(string roletname, int id)
		{
			List<string> errors = new List<string>();
			if (!this.FreeName(roletname, id))
				errors.Add("That name is already being used by another role");

			if (errors.Any())
			{
				string errosJson = JsonConvert.SerializeObject(errors);
				throw new BadRequestException(errosJson);
			}
		}


		public void Delete(int id)
		{
			Role role = _roleRepository.Get(id);
			role.deleted = true;
			_roleRepository.Update(role);
		}

		#region private
		public bool FreeName(string name, int id)
		{
			Role entity = _roleRepository.GetByName(name);

			if (entity != null && entity.id == id)
				return true;
			else
				return (entity == null);
		}
		#endregion
	}
}
