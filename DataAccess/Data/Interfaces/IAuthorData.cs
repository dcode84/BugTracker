using DataAccess.Models;

namespace DataAccess.Data.Interfaces;

public interface IAuthorData
{
    Task<AuthorModel?> GetAuthorAsync(int id);
    Task<IEnumerable<AuthorModel>> GetAuthorsAsync();
    Task<AuthorModel?> GetAuthorByNameAsync(AuthorModel author);
    Task CreateAuthorAsync(AuthorModel author);
    Task UpdateAuthorAsync(AuthorModel author);
}
