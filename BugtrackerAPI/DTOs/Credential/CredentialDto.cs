namespace BugtrackerAPI.DTOs.Credential;

public record CredentialDto
{
    public string PasswordHash { get; init; }
    public string PasswordSalt { get; init; }
    public DateTimeOffset? ModifiedAt { get; init; }
}
