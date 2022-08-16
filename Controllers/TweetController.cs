using System.Net;
using backend_api.Models;
using backend_api.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace backend_api.Controllers;

[ApiController]
[Route("[controller]")]
public class TweetController : ControllerBase

{
    private readonly ILogger<TweetController> _logger;
    private readonly ITweetRepository _tweetRepository;

    public TweetController(ILogger<TweetController> logger, ITweetRepository repo)
    {
        _logger = logger;
        _tweetRepository = repo;
    }

    [HttpGet]
    public IEnumerable<Tweet> GetAllTweets()
    {
       return _tweetRepository.GetAllTweets();
    }

    [HttpPost]
    public Tweet CreateTweet(Tweet tweet) {
        var newTweet = _tweetRepository.CreateTweet(tweet);
        return newTweet;
    }

    [HttpPut, Route("{tweetId}")]
    public Tweet? EditTweet(Tweet tweet) {
            var newTweet = _tweetRepository.EditTweet(tweet);
            if (tweet == null || !ModelState.IsValid)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return null;
            }
            return newTweet;
    }

    [HttpDelete, Route ("{tweetId}")]
    public void DeleteTweet(string tweetId) {
        _tweetRepository.DeleteTweet(tweetId);
        Response.StatusCode = (int) HttpStatusCode.NoContent;
    }

    

}
