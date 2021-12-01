﻿using DataAccess.DbAccess;
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

    public Task<IEnumerable<ProjectUserModel>> GetProjectUsers() =>
        _db.LoadData<ProjectUserModel, dynamic>("sp_readProjectUsers", new { });

    public Task<IEnumerable<ProjectUserModel>> GetProjectUsersByUser(int id) =>
        _db.LoadData<ProjectUserModel, dynamic>("sp_readProjectUsersByUser", new { userId = id });

    public Task<IEnumerable<ProjectUserModel>> GetProjectUsersByProject(int id) =>
        _db.LoadData<ProjectUserModel, dynamic>("sp_readProjectUsersByProject", new { projectId = id });

    public Task CreateProjectUser(int userId, int projectId) =>
        _db.SaveData("sp_createProjectUser", new { userId, projectId });

    public Task DeleteProjectUser(int userId, int projectId) =>
        _db.SaveData("sp_deleteProjectUser", new { userId, projectId });

}
