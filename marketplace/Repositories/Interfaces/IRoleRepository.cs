using marketplace.Models;

namespace marketplace.Repositories.Interfaces
{
	public interface IRoleRepository : IGenericRepository<Role>
	{
		public List<Role> GetAll();
		public bool FreeName(string name, int id);

	}
}
