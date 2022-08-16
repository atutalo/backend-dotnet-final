using backend_api.Models;

namespace backend_api.Repositories
{
    public class MockTweetRepository : ITweetRepository
    {
        private List<Tweet> mockTweets;
        //constructor that initiates a the list of mock tweets when the application is created
        public MockTweetRepository()
        {
        mockTweets = new List<Tweet>
        {
        new Tweet { tweetId = "1", Description = "This is my test tweet", User = "alttutalo", Date = "3/18/2022", Time = "3:55:05" },
        new Tweet { tweetId = "2", Description = "This is my second tweet", User = "alttutalo", Date = "3/02/2021", Time = "3:55:05" },
        new Tweet { tweetId = "3", Description = "This is my third tweet", User = "mandalynne", Date = "1/19/2022", Time = "3:55:05" }
        };

        //REST Methods
        }
        public IEnumerable<Tweet> GetAllTweets()
        {
            return mockTweets;
        }
        public Tweet CreateTweet(Tweet newTweet)
        {
            var maxId = mockTweets.Select(t => t.tweetId).DefaultIfEmpty().Max();
            newTweet.tweetId = maxId + 1;
            mockTweets.Add(newTweet);
            return newTweet;
        }
        public Tweet EditTweet(Tweet newTweet)
        {
           var tweet = mockTweets.FirstOrDefault(t => t.tweetId == newTweet.tweetId);
            if (tweet != null) {
                tweet.Description = newTweet.Description;
                tweet.User = newTweet.User;
                tweet.Date = newTweet.Date;
                tweet.Time = newTweet.Time;
            }
            return tweet;
        }
        public void DeleteTweet(string tweetId)
        {
            var tweet = mockTweets.FirstOrDefault(t => t.tweetId == tweetId);
            if (tweet != null)
            {
                mockTweets.Remove(tweet);
            }
        }
    }

}