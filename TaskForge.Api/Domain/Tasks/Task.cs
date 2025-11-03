using TaskForge.Api.Domain.Abstractions;

namespace TaskForge.Api.Domain.Tasks;

public sealed class Task : Entity
{
    private Task(
        Guid id,
        Guid projectId,
        TaskTitle title,
        TaskStatus status) : base(id)
    {
        ProjectId = projectId;
        Title = title;
        Status = status;
    }

    private Task()
    {
    }

    public Guid ProjectId { get; private set; }
    public TaskTitle Title { get; private set; }
    public TaskStatus Status { get; private set; }

    public static Task Create(
        Guid projectId,
        TaskTitle title)
    {
        var task = new Task(
            Guid.NewGuid(),
            projectId,
            title,
            TaskStatus.InProgress);

        return task;
    }

    public Result Update(TaskTitle? taskTitle, TaskStatus? taskStatus)
    {
        var hasChanges = false;

        if (taskTitle is not null && taskTitle != Title)
        {
            Title = taskTitle;
            hasChanges = true;
        }

        if (taskStatus is not null && taskStatus != Status)
        {
            Status = taskStatus.Value;
            hasChanges = true;
        }

        return hasChanges
            ? Result.Success()
            : Result.Failure(TaskErrors.NoChanges);
    }
}