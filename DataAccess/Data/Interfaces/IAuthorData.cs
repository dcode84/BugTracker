using DataAccess.Models;

namespace DataAccess.Data.Interfaces;

public interface IAuthorData
{
    Task<Author?> GetAuthorAsync(int id);
    Task<IEnumerable<Author>> GetAuthorsAsync();
    Task<Author?> GetAuthorByNameAsync(Author author);
    Task CreateAuthorAsync(Author author);
    Task UpdateAuthorAsync(Author author);
}
