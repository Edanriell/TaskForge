using TaskForge.Api.Domain.Abstractions;

namespace TaskForge.Api.Domain.Tasks;

public sealed record TaskTitle
{
    private const int MaxTaskTitleLength = 48;

    public static readonly Error InvalidName = new(
        "TaskTitle.InvalidName",
        "Task title cannot be empty or null"
    );

    public static readonly Error InvalidLength = new(
        "TaskTitle.InvalidLength",
        $"Task title cannot exceed {MaxTaskTitleLength} characters"
    );

    private TaskTitle(string value)
    {
        Value = value;
    }

    public string Value { get; init; }

    public static Result<TaskTitle> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value)) return Result.Failure<TaskTitle>(InvalidName);

        if (value.Length > MaxTaskTitleLength) return Result.Failure<TaskTitle>(InvalidLength);

        return new TaskTitle(value);
    }

    public static TaskTitle FromValue(string value)
    {
        return new TaskTitle(value);
    }
}