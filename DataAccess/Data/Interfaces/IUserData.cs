using DataAccess.Models;

namespace DataAccess.Data.Interfaces;

public interface IUserData
{
    Task CreateUserAsync(UserModel user);
    Task DeleteUserAsync(int id);
    Task<UserModel?> GetUserByNameAsync(string username);
    Task<UserModel?> GetUserAsync(int id);
    Task<IEnumerable<UserModel>> GetUsersAsync();
    Task UpdateUserAsync(UserModel user);
}
