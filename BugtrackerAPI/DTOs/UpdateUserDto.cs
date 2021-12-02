using System.ComponentModel.DataAnnotations;

namespace BugtrackerAPI.DTOs;

public class UpdateUserDto
{
    [Required]
    public int Id { get; init; }

    [Required]
    [MinLength(6)]
    [MaxLength(32)]
    public string Username { get; init; }

    [Required]
    [EmailAddress]
    [MaxLength(64)]
    public string Email { get; init; }
}
