using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace backend_api.Models 
{
    public class Tweet {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? tweetId {get; set;}
        [BsonElement("Description")]
        public string? Description {get; set;}
        [BsonElement("User")]
        public string? User {get; set;}
        [BsonElement("Date")]
        public string? Date { get; set; }
        [BsonElement("Time")]
        public string? Time {get; set ;}
        }
}


