using backend_api.Models;

namespace backend_api.Repositories

{
    public interface ITweetRepository
    {
        IEnumerable<Tweet> GetAllTweets();
        Tweet CreateTweet(Tweet newTweet);
        Tweet EditTweet(Tweet newTweet);
         void DeleteTweet(string tweetId);

    }
}