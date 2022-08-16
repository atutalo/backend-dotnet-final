namespace backend_api.Repositories
{
    public class TweeterDatabaseSettings : ITweeterDatabaseSettings
    {
        public string TweetCollectionName {get; set;} = null!;
        public string ConnectionString {get; set;} = null!;
        public string DatabaseName {get; set;} = null!;
    }

    public interface ITweeterDatabaseSettings
    {
        string TweetCollectionName {get; set;}
        string ConnectionString {get; set;}
        string DatabaseName {get; set;}
    }
}