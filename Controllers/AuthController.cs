using backend_api.Models;
using backend_api.Repositories;
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

    // [HttpGet]
    // public async Task<ActionResult<IEnumerable<User>>> GetAllUsers()
    // {
    //     return Ok(await _authService.GetAllUsers());
    // }

    [HttpPost]
    [Route("register")]
    public ActionResult CreateUser(User user)
    {
       if (user == null || !ModelState.IsValid)
        {
            return BadRequest();
        }
         _authService.CreateUser(user);
        return NoContent();
        //redirect to the login page
    }

    [HttpPost]
    [Route("signin")]
    public ActionResult<string> SignIn(SignInRequest result)
    {
        //Validating that the email and password were passed in - if not, it will return 400 Bad Request
        if (string.IsNullOrWhiteSpace(result.Email) || string.IsNullOrWhiteSpace(result.Password))
        {
            return BadRequest();
        }

        //When signed in, we get a token from AuthService
        var token = _authService.SignIn(result).ToString();
        //Auth service will return an empty string if the passwords do not match = 401 Unauthorized 
        if (string.IsNullOrWhiteSpace(token))
        {
            return Unauthorized();
        }
        //If the passwords match, the token will be returned
        return Ok(token);
    }
}