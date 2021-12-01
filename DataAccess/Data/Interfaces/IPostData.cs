using DataAccess.Models;

namespace DataAccess.Data.Interfaces;

public interface IPostData
{
    Task CreatePost(PostModel post);
    Task DeletePost(int id);
    Task<PostModel?> GetPost(int id);
    Task<IEnumerable<PostModel>> GetPosts(int projectId);
    Task UpdatePost(PostModel post);
}
