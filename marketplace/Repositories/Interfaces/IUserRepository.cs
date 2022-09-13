using marketplace.DTO.UserDTO;
using marketplace.Models;

namespace marketplace.Repositories.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        User AuthenticateUser(LoginDTO loginCredentials);

        bool FreeUsername(string username, int id);
        bool FreeEmail(string email, int id);

        User GetByEmail(string email);

        List<User> GetAll();

    }
}
