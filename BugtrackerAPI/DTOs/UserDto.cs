namespace BugtrackerAPI.DTOs;

public class UserDto
{
    public string UserName { get; init; }
    public string Email { get; init; }
    public DateTimeOffset DateCreated { get; init; }
    public bool IsValidated { get; init; }
    public string FirstName { get; init; }
    public string LastName { get; init; }

}
