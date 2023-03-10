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

    public Task<IEnumerable<User>> GetUsersAsync() =>
        _db.LoadData<User, dynamic>("sp_readUsers", new { });

    public async Task<User?> GetUserAsync(int id)
    {
        var results = await _db.LoadData<User, dynamic>("sp_readUser", new { userId = id });

        return results.FirstOrDefault();
    }

    public async Task<User?> GetUserByNameAsync(string username)
    {
        var results = await _db.LoadData<User, dynamic>("sp_readUserByName", new { username });

        return results.FirstOrDefault();
    }

    public Task CreateUserAsync(User user) =>
        _db.SaveData("sp_createUser",
            new
            {
                user.Username,
                user.Email
            });

    public Task UpdateUserAsync(User user) =>
        _db.SaveData("sp_updateUser", 
            new 
            { 
                userId = user.Id, 
                username = user.Username, 
                email = user.Email
            });

    public Task DeleteUserAsync(int id) =>
        _db.SaveData("sp_deleteUser", new { userId = id });

}
