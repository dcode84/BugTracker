using DataAccess.DbAccess;
using DataAccess.Models;
using DataAccess.Data.Interfaces;


namespace DataAccess.Data;

public class CredentialData : ICredentialData
{
    private readonly IMySqlDataAccess _db;

    public CredentialData(IMySqlDataAccess db)
    {
        _db = db;
    }

    public async Task<CredentialModel?> GetCredential(int id)
    {
        var results = await _db.LoadData<CredentialModel, dynamic>("sp_readCredential", new { userId = id });

        return results.FirstOrDefault();
    }

    public Task UpdateCredential(CredentialModel credentials) =>
        _db.SaveData("sp_updateCredentials", credentials);
}
