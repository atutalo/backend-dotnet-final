using backend_api.Models;
using backend_api.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend_api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly ILogger<AuthController> _logger;
    private readonly IAuthService _authService;

    public AuthController(ILogger<AuthController> logger, IAuthService authService)
    {
        _logger = logger;
        _authService = authService;
    }

    [HttpPost]
    [Route("register")]
    public async Task<ActionResult> CreateUser(User user)
    {
       if (user == null || !ModelState.IsValid)
        {
            return BadRequest();
        }
        var result =  await _authService.CreateUser(user);
        return NoContent();
        //redirect to the login page
    }

    [HttpGet]
    [Route("signin")]
    public async Task<IActionResult> SignIn(String email, String password)
    {
        //Validating that the email and password were passed in - if not, it will return 400 Bad Request
        if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
        {
            return BadRequest();
        }

        //When signed in, we get a token from AuthService
        var myToken = await _authService.SignIn(email, password);
        //Auth service will return an empty string if the passwords do not match = 401 Unauthorized 
        if (string.IsNullOrWhiteSpace(myToken))
        {
            return Unauthorized();
        }
        //If the passwords match, the token will be returned
        return Ok(myToken);
    }

    [HttpGet]
    [Route("current")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<ActionResult<User>> GetCurrentUser()
    {
        if (HttpContext.User == null) {
            return Unauthorized();
        }
        var currentClaim = HttpContext.User.Claims.FirstOrDefault(claim => claim.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"); 
        var currentUsername = currentClaim.Value;

        Console.WriteLine(currentUsername);
        var currentUser = await _authService.GetUserByUsername(currentUsername);

        return Ok(currentUser);
    }

    [HttpGet, Route("{username}")]
    public async Task<ActionResult<User>> GetUserByUsername(string username)
    {
        var currentProfile = await _authService.GetUserByUsername(username);
        return currentProfile;
    }

}