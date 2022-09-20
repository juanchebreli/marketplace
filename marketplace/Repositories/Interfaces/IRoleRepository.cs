using marketplace.Models;

namespace marketplace.Repositories.Interfaces
{
	public interface IRoleRepository : IGenericRepository<Role>
	{
		List<Role> GetAll();
		Role GetByName(string name);
	}
}
