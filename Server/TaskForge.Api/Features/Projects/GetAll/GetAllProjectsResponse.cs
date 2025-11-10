namespace TaskForge.Api.Features.Projects.GetAll;

public sealed record GetAllProjectsResponse
{
    public IReadOnlyList<ProjectResponse> Projects { get; init; }
}

public sealed record ProjectResponse
{
    public Guid Id { get; init; }
    public string Name { get; init; }
    public string Description { get; init; }
    public DateTime CreatedAt { get; init; }
    public IReadOnlyList<ProjectTaskResponse> Tasks { get; init; }
}

public sealed record ProjectTaskResponse
{
    public Guid Id { get; init; }
    public string Title { get; init; }
    public string Status { get; init; }
}