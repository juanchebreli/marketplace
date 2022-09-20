using marketplace.Models;

namespace marketplace.Repositories.Interfaces
{
	public interface IPermissionRepository : IGenericRepository<Permission>
	{
		List<Permission> GetAll();
		Permission GetByName(string name);

	}
}
