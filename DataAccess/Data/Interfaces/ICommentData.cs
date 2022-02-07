using DataAccess.Models;

namespace DataAccess.Data.Interfaces;

public interface ICommentData
{
    Task CreateComment(Comment comment);
    Task DeleteComment(int commentId);
    Task<Comment?> GetComment(int commentId);
    Task<IEnumerable<Comment>> GetComments(Post post);
    Task UpdateComment(User user, Comment comment);
}
