using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models;

public record PostModel
{
    public int Id { get; init; }
    public string Title { get; init; }
    public string Body { get; init; }
    public DateTimeOffset? DateCreated { get; init; }
    public bool IsDone { get; init; }
    public int ProjectId { get; init; }
    public int AuthorId { get; init; }
    public int PriorityId { get; init; }
    public int IssueTypeId { get; init; }
    public DateTimeOffset? ModifiedAt { get; init; }
    public int ModifiedBy { get; init; }
    public DateTimeOffset? DeletedAt { get; init; }
}
