using System.ComponentModel.DataAnnotations;

namespace BugtrackerAPI.DTOs.User;

public class CreateUserDto
{
    [Required]
    [MinLength(6)]
    [MaxLength(32)]
    public string Username { get; init; }

    [Required]
    [EmailAddress]
    [MaxLength(64)]
    public string Email { get; init; }
}
