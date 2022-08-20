using backend_api.Models;

namespace backend_api.Repositories;

public interface IAuthService
{
    User CreateUser(User user);
    Task<string> SignIn(String email, String password);
    User GetSignedInUser(string username);
}