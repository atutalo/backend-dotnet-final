using backend_api.Models;

namespace backend_api.Repositories;

public interface IAuthService
{
    User CreateUser(User user);
    Task<string> SignIn(SignInRequest request);
    //Task<User> GetSignedInUser(string username);
}