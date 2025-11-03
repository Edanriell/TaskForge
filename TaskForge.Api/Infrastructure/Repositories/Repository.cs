using TaskForge.Api.Domain.Abstractions;

namespace TaskForge.Api.Infrastructure.Repositories;

public abstract class Repository<T> where T : AggregateRoot
{
    protected readonly ApplicationDbContext DbContext;

    protected Repository(ApplicationDbContext dbContext)
    {
        DbContext = dbContext;
    }

    public virtual void Add(T entity)
    {
        DbContext.Add(entity);
    }

    public virtual void Update(T entity)
    {
        DbContext.Update(entity);
    }

    public virtual void Remove(T entity)
    {
        DbContext.Remove(entity);
    }
}