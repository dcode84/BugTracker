using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BugtrackerAPI.DTOs.Credential;

public record UpdateCredentialDto
{
    [Required]
    public int Id { get; init;}

    [Required]
    [MinLength(128)]
    [MaxLength(128)]
    public string PasswordSalt { get; init; }

    [Required]
    [MinLength(128)]
    [MaxLength(128)]
    public string PasswordHash { get; init; }
}
