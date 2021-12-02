using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models;

public record CredentialModel
{
    public int Id { get; init; }
    public string PasswordSalt { get; init; }
    public string PasswordHash { get; init; }
    public DateTimeOffset? ModifiedAt { get; init; }

}
