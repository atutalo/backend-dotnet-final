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

        public async Task<Tweet> CreateTweet(Tweet newTweet)
        {
            try {
                await _tweets.InsertOneAsync(newTweet);
            } catch (Exception ex) {
                Console.WriteLine(ex.ToString());
            }
            return newTweet;
        }

        public async Task DeleteTweet(string tweetId)
        {
            await _tweets.DeleteOneAsync(tweet => tweet.tweetId == tweetId);
        }

        public async Task<Tweet> EditTweet(Tweet newTweet)
        {
            await _tweets.ReplaceOneAsync(tweet => tweet.tweetId == newTweet.tweetId, newTweet);
            return newTweet;
        }

        public async Task<IEnumerable<Tweet>> GetAllTweets()
        {
            var allTweets = await _tweets.FindAsync(tweet => true);
            return allTweets.ToList();
        }

    }
}