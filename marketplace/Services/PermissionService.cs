using marketplace.MappingConfiguration;
using AutoMapper;
using marketplace.DTO.PermissionDTO;
using marketplace.Helpers;
using marketplace.Models;
using marketplace.Repositories;

namespace marketplace.Services
{
	public interface IPermissionService
	{
		List<Permission> GetAll();
		Permission Get(int id);
		Permission Add(PermissionCreateDTO entity);
		Permission Update(PermissionUpdateDTO entity);
		Permission Update(Permission entity);
		List<string> Validations(string permissionName, int id);
		void Delete(int id);
	}

	public class PermissionService : IPermissionService
	{
		private readonly IPermissionRepository _permissionRepository;
		private readonly IConfiguration _configuration;

		public PermissionService(IPermissionRepository permissionRepository, IConfiguration configuration)
		{

			_permissionRepository = permissionRepository;
			_configuration = configuration;

		}

		public List<Permission> GetAll()
		{
			return _permissionRepository.GetAll();
		}

		public Permission Get(int id)
		{
			return _permissionRepository.Get(id);
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
		public List<string> Validations(string permissiontname, int id)
		{
			List<string> errors = new List<string>();
			if (!_permissionRepository.FreeName(permissiontname, id))
				errors.Add("That name is already being used by another permission");
			return errors;
		}


		public void Delete(int id)
		{
			Permission permission = _permissionRepository.Get(id);
			permission.deleted = true;
			_permissionRepository.Update(permission);
		}
	}
}
