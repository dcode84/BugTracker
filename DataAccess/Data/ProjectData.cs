using DataAccess.DbAccess;
using DataAccess.Models;
using DataAccess.Data.Interfaces;


namespace DataAccess.Data;

public class ProjectData : IProjectData
{
    private readonly IMySqlDataAccess _db;

    public ProjectData(IMySqlDataAccess db)
    {
        _db = db;
    }

    public Task<IEnumerable<Project>> GetProjects() =>
        _db.LoadData<Project, dynamic>("sp_readProjects", new { });

    public async Task<Project?> GetProject(int id)
    {
        var results = await _db.LoadData<Project, dynamic>("sp_readProject", new { projectId = id });

        return results.FirstOrDefault();
    }

    public Task CreateProject(Project project) =>
        _db.SaveData("sp_createProject", new
        {
            project.Id,
            project.Name,
            project.Description
        });

    public Task UpdateProject(Project project) =>
        _db.SaveData("sp_updateProject", project);

    public Task DeleteProject(int id) =>
        _db.SaveData("sp_deleteProject", new { projectId = id });
}
