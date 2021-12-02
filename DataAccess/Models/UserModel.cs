namespace DataAccess.Models;

public record UserModel
{
    public int Id { get; init; }
    public string Username { get; init; }
    public string Email { get; init; }
    public DateTimeOffset? DateCreated { get; init; }
    public bool IsValidated { get; init; }
    public DateTimeOffset? ModifiedAt { get; init;}
    public DateTimeOffset? DeletedAt { get; init; }
}
