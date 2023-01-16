using System.ComponentModel.DataAnnotations;

namespace BugtrackerAPI.DTOs.UserRole;

public record DeleteUserRoleDto
{
    [Required]
    public int UserId { get; init; }

    [Required]
    public int RoleId { get; init; }

}
