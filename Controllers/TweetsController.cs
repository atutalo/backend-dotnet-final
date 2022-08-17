using backend_api.Models;
using backend_api.Repositories;
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
    [HttpPost]
    public async Task<ActionResult<Tweet>> CreateTweet(Tweet tweet) {
        if (!ModelState.IsValid || tweet == null)
        {
            return BadRequest();
        }
        return Ok (await _tweetRepository.CreateTweet(tweet));
    }

    [HttpPut, Route("{tweetId}")]
    public async Task<ActionResult<Tweet>> EditTweet(Tweet tweet) {
            if (tweet == null || !ModelState.IsValid)
            {
                return BadRequest();
            }
            return Ok(await _tweetRepository.EditTweet(tweet));
    }

    [HttpDelete, Route ("{tweetId}")]
    public async Task<ActionResult> DeleteTweet(string tweetId) {
        await _tweetRepository.DeleteTweet(tweetId);
        return NoContent();    
        }

    

}
