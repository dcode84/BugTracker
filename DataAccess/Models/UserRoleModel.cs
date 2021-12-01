using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models;

public record UserRoleModel
{
    public int UserId { get; init; }
    public int RoleId { get; init; }

}
