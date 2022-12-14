using backend_api.Models;

namespace backend_api.Repositories;

public interface IAuthService
{
    Task<User> CreateUser(User user);
    Task<string> SignIn(String email, String password);
    Task<User> GetUserByUsername(string username);
}