using DataAccess.Models;

namespace DataAccess.Data.Interfaces;

public interface IPostData
{
    Task CreatePost(Post post);
    Task DeletePost(int id);
    Task<Post?> GetPost(int id);
    Task<IEnumerable<Post>> GetPosts(int projectId);
    Task UpdatePost(Post post);
}
