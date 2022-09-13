using marketplace.Models;

namespace marketplace.Repositories.Interfaces
{
	public interface IPermissionRepository : IGenericRepository<Permission>
	{
		public List<Permission> GetAll();
		public bool FreeName(string name, int id);

	}
}
