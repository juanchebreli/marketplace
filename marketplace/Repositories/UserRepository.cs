using marketplace.Context;
using marketplace.DTO.UserDTO;
using marketplace.Models;

namespace marketplace.Repositories
{
    public interface IUserRepository : IGenericRepository<User>
    {
        User AuthenticateUser(LoginDTO loginCredentials);

        bool FreeUsername(string username, int id);
        bool FreeEmail(string email, int id);

        User GetByEmail(string email);

    }
    public class UserRepository : GenericRepository<User, AppDbContext>, IUserRepository
    {
        readonly AppDbContext AppDbContext;

        public UserRepository(AppDbContext dbContext): base(dbContext)
        {
            AppDbContext = dbContext;
;
        }

        public User AuthenticateUser(LoginDTO loginCredentials)
        {
            User user = AppDbContext.Users.FirstOrDefault(x => x.username == loginCredentials.username && x.password == loginCredentials.password);
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
    }
}
