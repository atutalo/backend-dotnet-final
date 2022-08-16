using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace l11_tokens.Models;

public class User
{
    [JsonIgnore]
    public int UserId { get; set; }
    [Required]
    public string? Username {get; set;}
    [Required]
    public string? Password { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
  
}