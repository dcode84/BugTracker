using System.ComponentModel.DataAnnotations;

namespace BugtrackerAPI.DTOs.User;

public record CreateUserDto
{
    [Required(ErrorMessage = "Username is required")]
    [MinLength(6, ErrorMessage = "Must be 6 letters minimum.")]
    [MaxLength(32, ErrorMessage = "Must be 32 letters maximum.")]
    public string Username { get; init; }

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Email must be a valid email address.")]
    [MaxLength(64, ErrorMessage = "Must be 64 letters maximum.")]
    public string Email { get; init; }
}
