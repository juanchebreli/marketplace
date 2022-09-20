using marketplace.DTO.UserDTO;
using marketplace.Models;

namespace marketplace.Repositories.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        User AuthenticateUser(LoginDTO loginCredentials);

        User GetByEmail(string email);
        User GetByName(string name);
        User GetByUsername(string username);

        List<User> GetAll();

    }
}
