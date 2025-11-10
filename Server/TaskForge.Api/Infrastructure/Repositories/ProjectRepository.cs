using Microsoft.EntityFrameworkCore;
using TaskForge.Api.Domain.Projects;

namespace TaskForge.Api.Infrastructure.Repositories;

public sealed class ProjectRepository : Repository<Project>, IProjectRepository
{
    public ProjectRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<List<Project>> GetAllWithTasksAsync(CancellationToken cancellationToken = default)
    {
        return await DbContext
            .Set<Project>()
            .Include(project => project.Tasks)
            .OrderBy(project => project.Name)
            .AsSplitQuery()
            .ToListAsync(cancellationToken);
    }

    public async Task<Project?> GetByIdWithTasksAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await DbContext
            .Set<Project>()
            .Include(project => project.Tasks)
            .AsSplitQuery()
            .FirstOrDefaultAsync(task => task.Id == id, cancellationToken);
    }

    public async Task<(List<Project> Projects, int TotalCount)> GetPagedAndSortedAndFilteredWithOrWithoutTasksAsync(
        ProjectQueryOptions queryOptions,
        CancellationToken cancellationToken = default)
    {
        IQueryable<Project> query = DbContext.Set<Project>();

        if (queryOptions.IncludeTasks) query = query.Include(p => p.Tasks).AsSplitQuery();

        if (!string.IsNullOrWhiteSpace(queryOptions.SearchTerm))
        {
            var search = queryOptions.SearchTerm.ToLower();
            query = query.Where(p => p.Name.Value.ToLower().Contains(search));
        }

        if (queryOptions.CreatedAfter.HasValue)
            query = query.Where(p => p.CreatedAt >= queryOptions.CreatedAfter.Value);

        if (queryOptions.CreatedBefore.HasValue)
            query = query.Where(p => p.CreatedAt <= queryOptions.CreatedBefore.Value);

        var totalCount = await query.CountAsync(cancellationToken);

        query = queryOptions.SortBy?.ToLower() switch
        {
            "name" => queryOptions.SortDescending
                ? query.OrderByDescending(p => p.Name)
                : query.OrderBy(p => p.Name),

            "createdat" => queryOptions.SortDescending
                ? query.OrderByDescending(p => p.CreatedAt)
                : query.OrderBy(p => p.CreatedAt),

            _ => query.OrderBy(p => p.Name) // default
        };

        var skip = (queryOptions.Page - 1) * queryOptions.PageSize;
        query = query.Skip(skip).Take(queryOptions.PageSize);

        var projects = await query.ToListAsync(cancellationToken);

        return (projects, totalCount);
    }

    public async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await DbContext
            .Set<Project>()
            .AnyAsync(project => project.Id == id, cancellationToken);
    }
}