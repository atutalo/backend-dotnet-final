using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace backend_api.Models;

public class User
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? UserId { get; set; }
    [Required]
    [BsonElement("username")]
    public string? Username { get; set; }
    [Required]
    [BsonElement("password")]
    public string? Password { get; set; }
    [BsonElement("firstName")]
    public string? FirstName { get; set; }
    [BsonElement("lastName")]
    public string? LastName { get; set; }
    [BsonElement("location")]
    public string? Location { get; set; }
    [BsonElement("createdDate")]
    public string? CreatedDate { get; set; }
    [BsonElement("tweets")]
    public List<Tweet>? tweets {get; set;}

} 