using DataAccess.DbAccess;
using DataAccess.Models;
using DataAccess.Data.Interfaces;


namespace DataAccess.Data;

public class CommentData : ICommentData
{
    private readonly IMySqlDataAccess _db;

    public CommentData(IMySqlDataAccess db)
    {
        _db = db;
    }

    public Task<IEnumerable<CommentModel>> GetComments(PostModel post) =>
        _db.LoadData<CommentModel, dynamic>("sp_readComments", new 
        {
            postId = post.Id
        });

    public async Task<CommentModel?> GetComment(int commentId)
    {
        var results = await _db.LoadData<CommentModel, dynamic>("sp_readComment", new { commentId });

        return results.FirstOrDefault();
    }

    public Task CreateComment(CommentModel comment) =>
        _db.SaveData("sp_createComment", new
        {
            body = comment.Body,
            authorId = comment.AuthorId,
            postId = comment.PostId
        });

    public Task UpdateComment(UserModel user, CommentModel comment) =>
        _db.SaveData("sp_updateComment", new
        {
            userId = user.Id,
            commentId = comment.Id,
            body = comment.Body
        });

    public Task DeleteComment(int commentId) =>
        _db.SaveData("sp_deleteComment", new { commentId });
}
