using DataAccess.Models;

namespace DataAccess.Data.Interfaces;

public interface IProjectData
{
    Task CreateProject(ProjectModel project);
    Task DeleteProject(int id);
    Task<ProjectModel?> GetProject(int id);
    Task<IEnumerable<ProjectModel>> GetProjects();
    Task UpdateProject(ProjectModel project);
}
