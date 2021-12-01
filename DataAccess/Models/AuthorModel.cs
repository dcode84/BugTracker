namespace DataAccess.Models;

public record AuthorModel
{
    public int Id { get; init; }
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public DateTimeOffset ModifiedAt { get; init; }

}
