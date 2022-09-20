using marketplace.DTO.RoleDTO;
using marketplace.Models;

namespace marketplace.Services.Interfaces
{
	public interface IRoleService
	{
		List<Role> GetAll();
		Role Get(int id);
		Role Add(RoleCreateDTO entity);
		Role Update(RoleUpdateDTO entity);
		Role Update(Role entity);
		void Validate(string roleName, int id);
		void Delete(int id);
	}
}
