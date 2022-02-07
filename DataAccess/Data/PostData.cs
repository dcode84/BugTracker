using DataAccess.DbAccess;
using DataAccess.Models;
using DataAccess.Data.Interfaces;


namespace DataAccess.Data;

public class PostData : IPostData
{
    private readonly IMySqlDataAccess _db;

    public PostData(IMySqlDataAccess db)
    {
        _db = db;
    }

    public Task<IEnumerable<Post>> GetPosts(int projectId) =>
        _db.LoadData<Post, dynamic>("sp_readPosts", new { projectId });

    public async Task<Post?> GetPost(int id)
    {
        var results = await _db.LoadData<Post, dynamic>("sp_readPost", new { postId = id });

        return results.FirstOrDefault();
    }

    public Task CreatePost(Post post) =>
        _db.SaveData("sp_createPost", new
        {
            post.Title,
            post.Body,
            post.ProjectId,
            post.PriorityId,
            post.IssueTypeId,
            post.ModifiedBy
        });

    public Task UpdatePost(Post post) =>
        _db.SaveData("sp_updatePost", post);

    public Task DeletePost(int id) =>
        _db.SaveData("sp_deletePost", new { postId = id });
}
