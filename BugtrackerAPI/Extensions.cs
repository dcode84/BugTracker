using BugtrackerAPI.DTOs;

namespace BugtrackerAPI.Extensions;

public static class Extensions
{
    public static UserDto AsDto(this UserModel user)
    {
        return new UserDto
        {
            Username = user.Username,
            Email = user.Email,
            DateCreated = user.DateCreated,
            IsValidated = user.IsValidated,
            ModifiedAt = user.ModifiedAt
        };
    }
}
