using DataAccess.Models;

namespace DataAccess.Data.Interfaces;

public interface IUserData
{
    Task CreateUserAsync(User user);
    Task DeleteUserAsync(int id);
    Task<User?> GetUserByNameAsync(string username);
    Task<User?> GetUserAsync(int id);
    Task<IEnumerable<User>> GetUsersAsync();
    Task UpdateUserAsync(User user);
}
