using DataAccess.DbAccess;
using DataAccess.Models;
using DataAccess.Data.Interfaces;


namespace DataAccess.Data;

public class AuthorData : IAuthorData
{
    private readonly IMySqlDataAccess _db;

    public AuthorData(IMySqlDataAccess db)
    {
        _db = db;
    }

    public Task<IEnumerable<Author>> GetAuthorsAsync() =>
        _db.LoadData<Author, dynamic>("sp_readAuthors", new { });

    public async Task<Author?> GetAuthorAsync(int id)
    {
        var results = await _db.LoadData<Author, dynamic>("sp_readAuthor", new { authorId = id });

        return results.FirstOrDefault();
    }

    public async Task<Author?> GetAuthorByNameAsync(Author author)
    {
        var results = await _db.LoadData<Author, dynamic>("sp_readAuthorByName", 
            new 
            { 
                firstname = author.FirstName,
                lastname = author.LastName
            });

        return results.FirstOrDefault();
    }

    public Task CreateAuthorAsync(Author author) =>
        _db.SaveData("sp_createAuthor", new
        {
            firstname = author.FirstName,
            lastname = author.LastName
        });


    public Task UpdateAuthorAsync(Author author) =>
        _db.SaveData("sp_updateAuthor", new
        {
            userId = author.Id,
            firstname = author.FirstName,
            lastname = author.LastName
        });
}
