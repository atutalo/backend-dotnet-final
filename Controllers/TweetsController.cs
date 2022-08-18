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
    private readonly ITweetRepository _tweetRepository;

    public TweetsController(ILogger<TweetsController> logger, ITweetRepository repo)
    {
        _logger = logger;
        _tweetRepository = repo;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Tweet>>> GetAllTweets()
    {
       return Ok (await _tweetRepository.GetAllTweets());
    }

    
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpPost]
    public async Task<ActionResult<Tweet>> CreateTweet(Tweet tweet) {
        if (!ModelState.IsValid || tweet == null)
        {
            return BadRequest();
        }
        return Ok (await _tweetRepository.CreateTweet(tweet));
    }

   
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpPut, Route("{tweetId}")]
    public async Task<ActionResult<Tweet>> EditTweet(Tweet tweet) {
            if (tweet == null || !ModelState.IsValid)
            {
                return BadRequest();
            }
            return Ok(await _tweetRepository.EditTweet(tweet));
    }

   
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpDelete, Route("{tweetId}")]
    public async Task<ActionResult> DeleteTweet(string tweetId) {
        await _tweetRepository.DeleteTweet(tweetId);
        return NoContent();    
        }

}
