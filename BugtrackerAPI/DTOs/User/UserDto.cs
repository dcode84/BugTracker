namespace BugtrackerAPI.DTOs.User;

public record UserDto
{
    public string Username { get; init; }
    public string Email { get; init; }
    public DateTimeOffset? DateCreated { get; init; }
    public bool IsValidated { get; init; }
    public DateTimeOffset? ModifiedAt { get; init; }

}
