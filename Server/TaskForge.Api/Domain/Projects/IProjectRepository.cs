namespace TaskForge.Api.Domain.Projects;

public interface IProjectRepository
{
    public Task<List<Project>> GetAllWithTasksAsync(CancellationToken cancellationToken = default);
    public Task<Project?> GetByIdWithTasksAsync(Guid id, CancellationToken cancellationToken = default);

    Task<(List<Project> Projects, int TotalCount)> GetPagedAndSortedAndFilteredWithOrWithoutTasksAsync(
        ProjectQueryOptions queryOptions,
        CancellationToken cancellationToken = default);

    Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default);
    void Add(Project project);
    void Update(Project project);
    void Remove(Project project);
}

public sealed record ProjectQueryOptions(
    int Page,
    int PageSize,
    string? SearchTerm = null,
    string? SortBy = null,
    bool SortDescending = false,
    DateTime? CreatedAfter = null,
    DateTime? CreatedBefore = null,
    bool IncludeTasks = false
);