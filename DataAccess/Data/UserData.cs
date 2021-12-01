using DataAccess.DbAccess;
using DataAccess.Models;
using DataAccess.Data.Interfaces;


namespace DataAccess.Data;

public class UserData : IUserData
{
    private readonly IMySqlDataAccess _db;

    public UserData(IMySqlDataAccess db)
    {
        _db = db;
    }

    public Task<IEnumerable<UserModel>> GetUsers() =>
        _db.LoadData<UserModel, dynamic>("sp_readUsers", new { });

    public async Task<UserModel?> GetUser(int id)
    {
        var results = await _db.LoadData<UserModel, dynamic>("sp_readUser", new { userId = id });

        return results.FirstOrDefault();
    }

    public Task CreateUser(UserModel user, AuthorModel author, CredentialModel credential) =>
        _db.SaveData("sp_createUser",
            new
            {
                user.Username,
                user.Email,
                author.FirstName,
                author.LastName,
                credential.PasswordSalt,
                credential.PasswordHash
            });

    public Task UpdateUser(UserModel user) =>
        _db.SaveData("sp_updateUser", user);

    public Task DeleteUser(int id) =>
        _db.SaveData("sp_deleteUser", new { userId = id });
}
