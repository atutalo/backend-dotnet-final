using backend_api.Models;

namespace backend_api.Repositories;

public interface ITweetService
{
    Task<IEnumerable<Tweet>> GetAllTweets();
    Task<IEnumerable<Tweet>> GetMyTweets(string user);
    Task<IEnumerable<Tweet>> GetTweetsByUsername(string user);
    Task<Tweet> EditTweet(string username, Tweet tweet);
    Task DeleteTweet(string tweetId);
    User CreateTweet(string username, Tweet tweet);
}