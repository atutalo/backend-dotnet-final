using backend_api.Models;

namespace backend_api.Repositories;

public interface ITweetService
{
    Task<IEnumerable<Tweet>> GetAllTweets();
    Task<Tweet> EditTweet(Tweet newTweet);
    Task DeleteTweet(string tweetId);
    User CreateTweet(string username, Tweet tweet);
}