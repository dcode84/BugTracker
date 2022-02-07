using BugtrackerAPI.DTOs.Author;
using BugtrackerAPI.DTOs.User;

namespace BugtrackerAPI.Extensions;

public static class Extensions
{
    public static UserDto UserAsDto(this User user)
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

    public static AuthorDto AuthorAsDto(this Author author)
    {
        return new AuthorDto
        {
            FirstName = author.FirstName,
            LastName = author.LastName,
            ModifiedAt = author.ModifiedAt
        };
    }
}
