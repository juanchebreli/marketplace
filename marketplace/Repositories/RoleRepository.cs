using marketplace.Context;
using marketplace.DTO.UserDTO;
using marketplace.Helpers;
using marketplace.Models;

namespace marketplace.Repositories
{
	public interface IRoleRepository : IGenericRepository<Role>
	{
		public List<Role> GetAll();
		public bool FreeName(string name, int id);

	}
	public class RoleRepository : GenericRepository<Role, AppDbContext>, IRoleRepository
	{
		readonly AppDbContext AppDbContext;

		public RoleRepository(AppDbContext dbContext) : base(dbContext)
		{
			AppDbContext = dbContext;
		}

		public List<Role> GetAll()
		{
			List<Role> roles = AppDbContext.Roles.Where(entity => entity.deleted == false).OrderBy(entity => entity.name).ToList();
			return roles;
		}

		public bool FreeName(string name, int id)
		{
			Role entity = AsNoTracking().FirstOrDefault(c => c.name == name && c.deleted == false);

			if (entity != null && entity.id == id)
				return true;
			else
				return (entity == null);
		}

	}
}
