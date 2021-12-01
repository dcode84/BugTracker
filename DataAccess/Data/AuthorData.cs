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

    public Task<IEnumerable<AuthorModel>> GetAuthors() =>
        _db.LoadData<AuthorModel, dynamic>("sp_readAuthors", new { });

    public async Task<AuthorModel?> GetAuthor(int id)
    {
        var results = await _db.LoadData<AuthorModel, dynamic>("sp_readAuthor", new { authorId = id });

        return results.FirstOrDefault();
    }

    public Task UpdateAuthor(AuthorModel author) =>
        _db.SaveData("sp_updateAuthor", new
        {
            authorId = author.Id,
            firstname = author.FirstName,
            lastname = author.LastName
        });
}
