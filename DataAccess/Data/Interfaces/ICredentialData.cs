using DataAccess.Models;

namespace DataAccess.Data.Interfaces;

public interface ICredentialData
{
    Task<Credential?> GetCredential(int id);
    Task UpdateCredential(Credential credentials);
}
