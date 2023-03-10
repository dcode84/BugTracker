using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models;

public record IssueType
{
    public int Id { get; init; }
    public string Name { get; init; }
    public string Description { get; init; }
}
