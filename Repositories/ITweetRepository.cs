using backend_api.Models;

namespace backend_api.Repositories

{
    public interface ITweetRepository
    {
        Task<IEnumerable<Tweet>> GetAllTweets();
        Task<Tweet> EditTweet(Tweet newTweet);
        Task DeleteTweet(string tweetId);
        User CreateTweet(string username, Tweet newTweet);
        //Task<IEnumerable<Tweet>> GetMyTweets();

    }
}