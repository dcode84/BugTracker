using System.ComponentModel.DataAnnotations;

namespace BugtrackerAPI.DTOs.Author;

public class UpdateAuthorDto
{
    [Required]
    public int Id { get; init; }

    [Required]
    [MaxLength(64)]
    public string FirstName { get; init; }

    [Required]
    [MaxLength(64)]
    public string LastName { get; init; }
}
