namespace DataAccess.Models;

public record CommentModel
{
    public int Id { get; init; }
    public string Body { get; init; }
    public DateTimeOffset? DateCreated { get; init; }
    public int AuthorId { get; init; }
    public int PostId { get; init; }
    public DateTimeOffset? ModifiedAt { get; init; }
    public int ModifiedBy { get; init; }
    public DateTimeOffset? DeletedAt { get; init; }
}
