namespace BugtrackerAPI.DTOs.Author;

public record AuthorDto
{
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public DateTimeOffset? ModifiedAt { get; init; }
}
