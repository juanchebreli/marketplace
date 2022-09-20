using marketplace.Context;
using marketplace.Models;
using marketplace.Repositories.Interfaces;

namespace marketplace.Repositories
{
	public class PermissionRepository : GenericRepository<Permission, AppDbContext>, IPermissionRepository
	{
		readonly AppDbContext AppDbContext;

		public PermissionRepository(AppDbContext dbContext) : base(dbContext)
		{
			AppDbContext = dbContext;
		}

		public List<Permission> GetAll()
		{
			List<Permission> permissions = AppDbContext.Permissions.Where(entity => entity.deleted == false).OrderBy(entity => entity.name).ToList();
			return permissions;
		}

		public Permission GetByName(string name)
		{
			return AppDbContext.Permissions.FirstOrDefault(c => c.name == name && c.deleted == false);
		}

	}
}
