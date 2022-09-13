using marketplace.Context;
using marketplace.Models;
using marketplace.Repositories.Interfaces;

namespace marketplace.Repositories
{
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
