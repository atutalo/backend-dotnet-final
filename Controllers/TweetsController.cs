using backend_api.Models;
using backend_api.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend_api.Controllers;

[ApiController]
[Route("tweets")]
public class TweetsController : ControllerBase

{
    private readonly ILogger<TweetsController> _logger;
    private readonly ITweetService _tweetService;

    public TweetsController(ILogger<TweetsController> logger, ITweetService service)
    {
        _logger = logger;
        _tweetService = service;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Tweet>>> GetAllTweets()
    {
       return Ok (await _tweetService.GetAllTweets());
    }

    [HttpGet, Route("{username}")]
    public async Task<ActionResult<IEnumerable<Tweet>>> GetTweetsByUser(string username)
    {
       return Ok (await _tweetService.GetTweetsByUsername(username));
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpGet, Route("myTweets")]
    public async Task<ActionResult<IEnumerable<Tweet>>> GetMyTweets()
    {
        var currentClaim = HttpContext.User.Claims.FirstOrDefault(claim => claim.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"); 
        var user = currentClaim.Value;
        return Ok (await _tweetService.GetMyTweets(user));
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpPut, Route("{tweetId}")]
    public async Task<ActionResult<Tweet>> EditTweet(Tweet tweet) {
            if (tweet == null || !ModelState.IsValid)
            {
                return BadRequest();
            }
            return Ok(await _tweetService.EditTweet(tweet));
    }

   [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpDelete, Route("{tweetId}")]
    public async Task<ActionResult> DeleteTweet(string tweetId) {
        await _tweetService.DeleteTweet(tweetId);
        return NoContent();    
        }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpPost]
    //pass in a tweet as the parameter and return an updated user
    public ActionResult<User> CreateTweet(Tweet tweet) {
        if (!ModelState.IsValid || tweet == null)
        {
            return BadRequest();
        }
        
            if (HttpContext.User == null) {
            return Unauthorized();
        }
        var currentClaim = HttpContext.User.Claims.FirstOrDefault(claim => claim.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"); 
        var currentUsername = currentClaim.Value;
        tweet.User = currentUsername;

        //call out to the tweetService CreateTweet(tweet);
        return Ok ( _tweetService.CreateTweet(currentUsername, tweet));
    }

}
