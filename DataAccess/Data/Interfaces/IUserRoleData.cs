using DataAccess.Models;

namespace DataAccess.Data.Interfaces;

public interface IUserRoleData
{
    Task CreateUserRole(int userId, int roleId);
    Task DeleteUserRole(int userId);
    Task DeleteUserRoles(int userId);
    Task<IEnumerable<UserRoleModel>> GetUserRoles(int userId);
}
