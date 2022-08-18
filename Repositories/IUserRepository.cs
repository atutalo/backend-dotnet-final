using backend_api.Models;

namespace backend_api.Repositories

{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllUsers();
        Task<User> CreateUser(User newUser);
        Task<User> GetUserByUsername(string username);
    }
}