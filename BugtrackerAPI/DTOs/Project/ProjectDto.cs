namespace BugtrackerAPI.DTOs.Project;

public record ProjectDto
{
    public int Id { get; init; }
    public string Name { get; init; }
    public string Description { get; init; }
    public DateTimeOffset? DateCreated { get; init; }
    public bool IsDone { get; init; }
    public int ManagerId { get; init; }
    public DateTimeOffset? ModifiedAt { get; init; }
    public int ModifiedBy { get; init; }
    public DateTimeOffset? DeletedAt { get; init; }
}
