using DataAccess.Models;

namespace DataAccess.Data.Interfaces;

public interface IProjectUserData
{
    Task CreateProjectUser(int userId, int projectId);
    Task DeleteProjectUser(int userId, int projectId);
    Task<IEnumerable<ProjectUser>> GetProjectUsers();
    Task<IEnumerable<ProjectUser>> GetProjectUsersByProject(int id);
    Task<IEnumerable<ProjectUser>> GetProjectUsersByUser(int id);
}
