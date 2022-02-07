using DataAccess.Models;

namespace DataAccess.Data.Interfaces;

public interface IProjectData
{
    Task CreateProject(Project project);
    Task DeleteProject(int id);
    Task<Project?> GetProject(int id);
    Task<IEnumerable<Project>> GetProjects();
    Task UpdateProject(Project project);
}
