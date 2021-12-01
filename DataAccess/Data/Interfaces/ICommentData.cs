using DataAccess.Models;

namespace DataAccess.Data.Interfaces;

public interface ICommentData
{
    Task CreateComment(CommentModel comment);
    Task DeleteComment(int commentId);
    Task<CommentModel?> GetComment(int commentId);
    Task<IEnumerable<CommentModel>> GetComments(PostModel post);
    Task UpdateComment(UserModel user, CommentModel comment);
}
