using System.ComponentModel.DataAnnotations;

namespace BugtrackerAPI.DTOs.UserRole;

public record CreateUserRoleDto
{
    [Required]
    public int UserId { get; init; }

    [Required]
    public int RoleId { get; init; }

}
