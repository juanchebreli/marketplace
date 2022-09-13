using marketplace.Context;
using marketplace.DTO.UserDTO;
using marketplace.Models;
using marketplace.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace marketplace.Repositories
{
    public class UserRepository : GenericRepository<User, AppDbContext>, IUserRepository
    {
        readonly AppDbContext AppDbContext;
		private readonly IConfiguration _configuration;

		public UserRepository(AppDbContext dbContext, IConfiguration configuration) : base(dbContext)
        {
            AppDbContext = dbContext;
			_configuration = configuration;
		}

        public User AuthenticateUser(LoginDTO loginCredentials)
        {
			string key = _configuration.GetSection("Encrypt")["Key"];
			User user = AppDbContext.Users.Where(x => x.username == loginCredentials.username).Include(user=> user.Role).FirstOrDefault();
            return user;
        }

        public bool FreeUsername(string usuario, int id)
        {
            User entity = AsNoTracking().FirstOrDefault(c => c.username == usuario && c.deleted == false);

            if (entity != null && entity.id == id)
                return true;
            else
                return (entity == null);
        }

        public bool FreeEmail(string email, int id)
        {
            User entity = AsNoTracking().FirstOrDefault(c => c.email == email && c.deleted == false);
            if (entity != null && entity.id == id)
                return true;
            else
                return (entity == null);
        }

        public User GetByEmail(string email)
        {
            return AsNoTracking().FirstOrDefault(c => c.email == email && c.deleted == false);
        }

        public List<User> GetAll()
        {
            List<User> users = AppDbContext.Users.Where(user => user.deleted == false).OrderBy(user => user.name).ToList();
            return users;
        }
    }
}
