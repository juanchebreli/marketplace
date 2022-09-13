using marketplace.DTO.PermissionDTO;
using marketplace.Models;

namespace marketplace.Services.Interfaces
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
}
