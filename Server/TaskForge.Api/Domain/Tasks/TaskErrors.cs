using TaskForge.Api.Domain.Abstractions;

namespace TaskForge.Api.Domain.Tasks;

public static class TaskErrors
{
    public static readonly Error DuplicateTask = new(
        "Task.DuplicateTask",
        "Duplicate tasks are not allowed to be added to the same project twice");

    public static readonly Error TaskNotFound = new(
        "Task.TaskNotFound",
        "The specified task could not be found");

    public static readonly Error CouldNotRemoveTask = new(
        "Task.CouldNotRemoveTask",
        "The task could not be removed");

    public static readonly Error NoChanges = new(
        "Task.NoChange",
        "No changes were made to the task");
}