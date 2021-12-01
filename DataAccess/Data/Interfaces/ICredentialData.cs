using DataAccess.Models;

namespace DataAccess.Data.Interfaces;

public interface ICredentialData
{
    Task<CredentialModel?> GetCredential(int id);
    Task UpdateCredential(CredentialModel credentials);
}
