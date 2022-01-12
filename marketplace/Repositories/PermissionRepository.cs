using marketplace.Context;
using marketplace.DTO.UserDTO;
using marketplace.Helpers;
using marketplace.Models;

namespace marketplace.Repositories
{
	public interface IPermissionRepository : IGenericRepository<Permission>
	{
		public List<Permission> GetAll();
		public bool FreeName(string name, int id);

	}
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

		public bool FreeName(string name, int id)
		{
			Permission entity = AsNoTracking().FirstOrDefault(c => c.name == name && c.deleted == false);

			if (entity != null && entity.id == id)
				return true;
			else
				return (entity == null);
		}

	}
}
