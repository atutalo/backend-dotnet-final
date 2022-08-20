using System.Text;
using backend_api.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace backend_api.Repositories;

public class TweetService : ITweetService
{
    private IUserRepository _userRepo;
    private ITweetRepository _tweetRepo;
    private IConfiguration _config;

    public TweetService(IUserRepository userRepo, IConfiguration config, ITweetRepository tweetRepo){
        _userRepo = userRepo; 
        _config = config;
        _tweetRepo = tweetRepo;
    }

    public async Task<IEnumerable<Tweet>> GetAllTweets()
    {
        var result = await _tweetRepo.GetAllTweets();
        return result;
    }

    public async Task<Tweet> EditTweet(Tweet newTweet)
    {
        var result = await _tweetRepo.EditTweet(newTweet);
        return result;
    }

    public async Task DeleteTweet(string tweetId)
    {
        await _tweetRepo.DeleteTweet(tweetId);
    }

     public User CreateTweet(string username, Tweet tweet)
    {
        var user = _tweetRepo.CreateTweet(username, tweet);
        return user;
    }

}

/*
     public async Task<User> CreateTweet(Tweet tweet)
     {
        var user = await_userRepo.
        var result = await _tweetRepo.CreateTweetForUser(tweet);
        return result;
     };

*/

