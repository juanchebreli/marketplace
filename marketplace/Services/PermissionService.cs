using marketplace.DTO.PermissionDTO;
using marketplace.Models;
using marketplace.Services.Interfaces;
using marketplace.Repositories.Interfaces;
using marketplace.Helpers.Exceptions.Implements;
using System.Text;
using Newtonsoft.Json;

namespace marketplace.Services
{
	

	public class PermissionService : IPermissionService
	{
		private readonly IPermissionRepository _permissionRepository;

		public PermissionService(IPermissionRepository permissionRepository)
		{

			_permissionRepository = permissionRepository;
		}

		public List<Permission> GetAll()
		{
			List<Permission> permissions = _permissionRepository.GetAll();
			if (permissions.Count == 0) throw new NoContentException("Don't have a permissions");
			return permissions;
		}

		public Permission Get(int id)
		{
			Permission permission = _permissionRepository.Get(id);
			if (permission == null) throw new NotFoundException(new StringBuilder("Not found a permission with id: {0}", id).ToString());
			return permission;
		}

		public Permission Add(PermissionCreateDTO entity)
		{
			return _permissionRepository.Add<PermissionCreateDTO, PermissionCreateDTO.MapperProfile>(entity);
		}

		public Permission Update(PermissionUpdateDTO entity)
		{
			return _permissionRepository.Update<PermissionUpdateDTO, PermissionUpdateDTO.MapperProfile>(entity);
		}

		public Permission Update(Permission entity)
		{
			return _permissionRepository.Update(entity);
		}
		public void Validate(string permissiontname, int id)
		{
			List<string> errors = new List<string>();
			if (!this.FreeName(permissiontname, id))
				errors.Add("That name is already being used by another permission");

			if (errors.Any())
			{
				string errosJson = JsonConvert.SerializeObject(errors);
				throw new BadRequestException(errosJson);
			}
		}


		public void Delete(int id)
		{
			Permission permission = _permissionRepository.Get(id);
			if (permission == null) throw new NotFoundException(new StringBuilder("Not found a permission with id: {0}", id).ToString());

			permission.deleted = true;
			_permissionRepository.Update(permission);
		}

		#region private
		public bool FreeName(string name, int id)
		{
			Permission entity = _permissionRepository.GetByName(name);

			if (entity != null && entity.id == id)
				return true;
			else
				return (entity == null);
		}
		#endregion
	}
}
