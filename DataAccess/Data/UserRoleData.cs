using DataAccess.DbAccess;
using DataAccess.Models;
using DataAccess.Data.Interfaces;

namespace DataAccess.Data;

public class UserRoleData : IUserRoleData
{
    private readonly IMySqlDataAccess _db;

    public UserRoleData(IMySqlDataAccess db)
    {
        _db = db;
    }

    public Task<IEnumerable<UserRole>> GetUserRoles(int userId) =>
        _db.LoadData<UserRole, dynamic>("sp_readRoles", new { userId });

    public Task CreateUserRole(int userId, int roleId) =>
        _db.SaveData("sp_createUserRole", new { userId, roleId });

    public Task DeleteUserRole(int userId) =>
        _db.SaveData("sp_deleteUserRole", new { userId });

    public Task DeleteUserRoles(int userId) =>
    _db.SaveData("sp_deleteUserRoles", new { userId });
}
