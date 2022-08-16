using backend_api.Models;
using MongoDB.Driver;

namespace backend_api.Repositories
{
    public class MongoTweeterRepository : ITweetRepository
    {
        //use IMongoCollection to access our collection of tweet objects called _tweets from MongoDb
        private readonly IMongoCollection<Tweet> _tweets;
        public MongoTweeterRepository(ITweeterDatabaseSettings settings)
        {
            //Pull the "Connection String" from the appsettings.json and initiate a new instance of the MongoClient class
            var client = new MongoClient(settings.ConnectionString);
            //Create a database using the "Database Name" from appsettings.json
            var database = client.GetDatabase(settings.DatabaseName);

            //Get the collection of Tweets from the database using the tweet collection name (save in _tweets variable)
            _tweets = database.GetCollection<Tweet>(settings.TweetCollectionName);
        }

        public Tweet CreateTweet(Tweet newTweet)
        {
            _tweets.InsertOne(newTweet);
            return newTweet;
        }

        public void DeleteTweet(string tweetId)
        {
            _tweets.DeleteOne(tweet => tweet.tweetId == tweetId);
        }

        public Tweet EditTweet(Tweet newTweet)
        {
            _tweets.ReplaceOne(tweet => tweet.tweetId == newTweet.tweetId, newTweet);
            return newTweet;
        }

        public IEnumerable<Tweet> GetAllTweets()
        {
            return _tweets.Find(tweet => true).ToList();
        }
    }
}