namespace BugtrackerAPI.DTOs.Author;

public class AuthorDto
{
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public DateTimeOffset? ModifiedAt { get; init; }
}
