using DataAccess.Models;

namespace DataAccess.Data.Interfaces;

public interface IProjectUserData
{
    Task CreateProjectUser(int userId, int projectId);
    Task DeleteProjectUser(int userId, int projectId);
    Task<IEnumerable<ProjectUserModel>> GetProjectUsers();
    Task<IEnumerable<ProjectUserModel>> GetProjectUsersByProject(int id);
    Task<IEnumerable<ProjectUserModel>> GetProjectUsersByUser(int id);
}
