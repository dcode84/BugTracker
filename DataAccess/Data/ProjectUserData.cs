using DataAccess.DbAccess;
using DataAccess.Models;
using DataAccess.Data.Interfaces;

namespace DataAccess.Data;

public class ProjectUserData : IProjectUserData
{
    private readonly IMySqlDataAccess _db;

    public ProjectUserData(IMySqlDataAccess db)
    {
        _db = db;
    }

    public Task<IEnumerable<ProjectUser>> GetProjectUsers() =>
        _db.LoadData<ProjectUser, dynamic>("sp_readProjectUsers", new { });

    public Task<IEnumerable<ProjectUser>> GetProjectUsersByUser(int id) =>
        _db.LoadData<ProjectUser, dynamic>("sp_readProjectUsersByUser", new { userId = id });

    public Task<IEnumerable<ProjectUser>> GetProjectUsersByProject(int id) =>
        _db.LoadData<ProjectUser, dynamic>("sp_readProjectUsersByProject", new { projectId = id });

    public Task CreateProjectUser(int userId, int projectId) =>
        _db.SaveData("sp_createProjectUser", new { userId, projectId });

    public Task DeleteProjectUser(int userId, int projectId) =>
        _db.SaveData("sp_deleteProjectUser", new { userId, projectId });

}
