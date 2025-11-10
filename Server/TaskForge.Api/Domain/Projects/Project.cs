using TaskForge.Api.Domain.Abstractions;
using TaskForge.Api.Domain.Tasks;
using Task = TaskForge.Api.Domain.Tasks.Task;
using TaskStatus = TaskForge.Api.Domain.Tasks.TaskStatus;

namespace TaskForge.Api.Domain.Projects;

public sealed class Project : AggregateRoot
{
    private readonly List<Task> _tasks = new();

    private Project(
        Guid id,
        ProjectName name,
        ProjectDescription description,
        DateTime createdAt) : base(id)
    {
        Name = name;
        Description = description;
        CreatedAt = createdAt;
    }

    private Project()
    {
    }

    public ProjectName Name { get; private set; }
    public ProjectDescription Description { get; private set; }
    public DateTime CreatedAt { get; private set; }

    public IReadOnlyList<Task> Tasks => _tasks.AsReadOnly();

    public static Result<Project> Create(
        ProjectName name,
        ProjectDescription description
    )
    {
        var project = new Project(
            Guid.NewGuid(),
            name,
            description,
            DateTime.Now);

        return Result.Success(project);
    }

    public Result Update(
        ProjectName? name,
        ProjectDescription? description)
    {
        var hasChanges = false;

        if (name is not null && name != Name)
        {
            hasChanges = true;
            Name = name;
        }

        if (description is not null && description != Description)
        {
            hasChanges = true;
            Description = description;
        }

        return hasChanges
            ? Result.Success()
            : Result.Failure(ProjectErrors.NoChanges);
    }

    public Result Delete()
    {
        return Result.Success();
    }

    public Result AddTask(Task task)
    {
        if (_tasks.Any(t => t.Title == task.Title))
            return Result.Failure(TaskErrors.DuplicateTask);

        _tasks.Add(task);

        return Result.Success();
    }

    public Result UpdateTask(Guid taskId, TaskTitle? title, TaskStatus? taskStatus)
    {
        var task = _tasks.FirstOrDefault(task => task.Id == taskId);

        if (task is null) return Result.Failure(TaskErrors.TaskNotFound);

        return task.Update(title, taskStatus);
    }

    public Result RemoveTask(Guid taskId)
    {
        var task = _tasks.FirstOrDefault(t => t.Id == taskId);

        if (task is null) return Result.Failure(TaskErrors.TaskNotFound);

        var removed = _tasks.Remove(task);
        return removed
            ? Result.Success()
            : Result.Failure(TaskErrors.CouldNotRemoveTask);
    }
}