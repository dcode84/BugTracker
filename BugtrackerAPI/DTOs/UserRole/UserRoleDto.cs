namespace BugtrackerAPI.DTOs.UserRole;

public record UserRoleDto
{
    public int UserId { get; init; }
    public int RoleId { get; init; }
}
