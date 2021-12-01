using DataAccess.Models;

namespace DataAccess.Data.Interfaces;

public interface IAuthorData
{
    Task<AuthorModel?> GetAuthor(int id);
    Task<IEnumerable<AuthorModel>> GetAuthors();
    Task UpdateAuthor(AuthorModel author);
}
