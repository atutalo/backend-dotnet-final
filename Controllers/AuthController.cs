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
    public ActionResult CreateUser(User user)
    {
       if (user == null || !ModelState.IsValid)
        {
            return BadRequest();
        }
        var result =  _authService.CreateUser(user);
        Console.WriteLine(result);
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

/*
    [HttpGet]
    [Route("current")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public ActionResult<User> GetCurrentUser()
    {
        var currentClaim = HttpContext.User.Claims.FirstOrDefault(claim => claim.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"); 
        var currentUsername = currentClaim.Value;

        if (user == null) {
            return Unauthorized();
        }

        var currentUser = await _authService.GetSignedInUser(currentUsername);
        return Ok(currentUser);
    }

    */

}