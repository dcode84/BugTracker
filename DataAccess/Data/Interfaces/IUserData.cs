using DataAccess.Models;

namespace DataAccess.Data.Interfaces;

public interface IUserData
{
    Task CreateUser(UserModel user, AuthorModel author, CredentialModel credential);
    Task DeleteUser(int id);
    Task<UserModel?> GetUser(int id);
    Task<IEnumerable<UserModel>> GetUsers();
    Task UpdateUser(UserModel user);
}
