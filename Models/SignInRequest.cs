using System.ComponentModel.DataAnnotations;

namespace backend_api.Models;

public class SignInRequest
{
    [Required]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }
}