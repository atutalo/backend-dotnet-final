using System.Text;
using backend_api.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace backend_api.Repositories;

public class AuthService : IAuthService
{
    private IUserRepository _userRepo;
    private IConfiguration _config;
    public AuthService(IUserRepository userRepo, IConfiguration config){
        _userRepo = userRepo; 
        _config = config;
    }
    public User CreateUser(User user)
    { 
        var passwordHash = BCrypt.Net.BCrypt.HashPassword(user.Password);
        user.Password = passwordHash;

        _userRepo.CreateUser(user);
        return user;
  
    }

    public async Task<string> SignIn(SignInRequest request)
    {
      //take the request and call the GetUsername method to get the specific User
        var user = await _userRepo.GetUserByUsername(request.Email)!;
        var verified = false;

        //verify the password is correct
        if (user != null)
        {
            verified = BCrypt.Net.BCrypt.Verify(request.Password, user.Password);
        }

        if (user == null || !verified)
        {
            return String.Empty;
        }
    
        //create JWT token and return - TODO
        var result = BuildToken(user);
        return result;

        
    }

    private string BuildToken(User user)
    {
        var secret = _config.GetValue<String>("TokenSecret");
        var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));

        // Create Signature using secret signing key
        var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

        // Create claims to add to JWT payload
        var claims = new Claim[]
        {
        new Claim(JwtRegisteredClaimNames.Sub, user.Username ?? ""),
        new Claim(JwtRegisteredClaimNames.FamilyName, user.LastName ?? ""),
        new Claim(JwtRegisteredClaimNames.GivenName, user.FirstName ?? "")
        };

        // Create token
        var jwt = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddMinutes(10),
            signingCredentials: signingCredentials);

        // Encode token
        var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

        return encodedJwt;
    }

    public User GetSignedInUser(string username)
    {
        var user = _userRepo.FindUser(username);
        return user;
    }
    
}