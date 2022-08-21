using backend_api.Models;
using MongoDB.Driver;

namespace backend_api.Repositories
{
    public class MongoTweeterRepository : ITweetRepository
    {
        //use IMongoCollection to access our collection of tweet objects called _tweets from MongoDb
        private readonly IMongoCollection<Tweet> _tweets;
        private readonly IMongoCollection<User> _users;


        public MongoTweeterRepository(ITweeterDatabaseSettings settings)
        {
            //Pull the "Connection String" from the appsettings.json and initiate a new instance of the MongoClient class
            var client = new MongoClient(settings.ConnectionString);
            //Create a database using the "Database Name" from appsettings.json
            var database = client.GetDatabase(settings.DatabaseName);

            //Get the collection of Tweets from the database using the tweet collection name (save in _tweets variable)
            _tweets = database.GetCollection<Tweet>(settings.TweetCollectionName);
            _users = database.GetCollection<User>(settings.UserCollectionName);
        }

        public async Task<IEnumerable<Tweet>> GetAllTweets()
        {
            var allTweets = await _tweets.FindAsync(tweet => true);
            return allTweets.ToList();
        }

        public async Task<IEnumerable<Tweet>> GetMyTweets(string username)
        {
            var findUsers = await _users.FindAsync(user => true);
            var currentUser = findUsers.ToList().FirstOrDefault(user => user.Username == username);
        
            if (currentUser.tweets == null) {
                currentUser.tweets = new List<Tweet>();
                return currentUser.tweets;
            }
            return currentUser.tweets;
        }

          public async Task<IEnumerable<Tweet>> GetTweetsByUsername(string username)
        {
            var findUsers = await _users.FindAsync(user => true);
            var currentUser = findUsers.ToList().FirstOrDefault(user => user.Username == username);

            if (currentUser.tweets == null) {
                currentUser.tweets = new List<Tweet>();
                return currentUser.tweets;
            }
            return currentUser.tweets;
        }

        public async Task DeleteTweet(string tweetId, string username)
        {
            var findUsers = await _users.FindAsync(user => true);
            var currentUser = findUsers.ToList().FirstOrDefault(user => user.Username == username);

            var tweetToRemove = currentUser.tweets.Find(Tweet => Tweet.tweetId == tweetId);
            currentUser.tweets.Remove(tweetToRemove);
            await _users.ReplaceOneAsync(user => user.Username == username, currentUser);
            await _tweets.DeleteOneAsync(tweet => tweet.tweetId == tweetId);
        }

        public async Task<Tweet> EditTweet(string username, Tweet newTweet)
        {
            var findUsers = await _users.FindAsync(user => true);
            var currentUser = findUsers.ToList().FirstOrDefault(user => user.Username == username);
            var tweetToRemove= currentUser.tweets.Find(Tweet => Tweet.tweetId == newTweet.tweetId);

            newTweet.Date = DateTime.Now.ToString("yyyy-MM-dd");
            newTweet.Time = DateTime.Now.ToString("hh:mm:ss");
            newTweet.User = username;
            await _tweets.ReplaceOneAsync(tweet => tweet.tweetId == newTweet.tweetId, newTweet);

            currentUser.tweets.Remove(tweetToRemove);
            currentUser.tweets.Add(newTweet);
            await _users.ReplaceOneAsync(user => user.Username == username, currentUser);
            

            return newTweet;
        }

        public User CreateTweet(string username, Tweet newTweet)
        {   
            newTweet.Date = DateTime.Now.ToString("yyyy-MM-dd");
            newTweet.Time = DateTime.Now.ToString("hh:mm:ss");
            newTweet.User = username;
            var findUsers = _users.Find(user => true);
            var currentUser = findUsers.ToList().FirstOrDefault(user => user.Username == username);
        
            _tweets.InsertOne(newTweet);
            if (currentUser.tweets == null)
            {
                currentUser.tweets = new List<Tweet>();
                currentUser.tweets.Add(newTweet);
                _users.ReplaceOne(user => user.Username == currentUser.Username, currentUser);
            } else {
                currentUser.tweets.Add(newTweet);
                _users.ReplaceOne(user => user.Username == currentUser.Username, currentUser);

            }
            return currentUser;
        }
    }
    
}