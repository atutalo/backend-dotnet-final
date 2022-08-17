using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace backend_api.Models;

public class User
{
    [Required]
    [BsonElement("Username")]
    public string? Username { get; set; }
    [Required]
    [BsonElement("Password")]
    public string? Password { get; set; }
    [BsonElement("FirstName")]
    public string? FirstName { get; set; }
    [BsonElement("LastName")]
    public string? LastName { get; set; }
    [BsonElement("Location")]
    public string? Location { get; set; }
} 