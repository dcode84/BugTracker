using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models;

public record ProjectUser
{
    public int ProjectId { get; init; }
    public int UserId { get; init; }
}
