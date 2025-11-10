using TaskForge.Api.Domain.Abstractions;

namespace TaskForge.Api.Domain.Projects;

public sealed record ProjectName
{
    private const int MaxProjectNameLength = 64;

    public static readonly Error InvalidName = new(
        "ProjectName.InvalidName",
        "Project name cannot be empty or null"
    );

    public static readonly Error InvalidLength = new(
        "ProjectName.InvalidLength",
        $"Project name cannot exceed {MaxProjectNameLength} characters"
    );

    private ProjectName(string value)
    {
        Value = value;
    }

    public string Value { get; init; }

    public static Result<ProjectName> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value)) return Result.Failure<ProjectName>(InvalidName);

        if (value.Length > MaxProjectNameLength) return Result.Failure<ProjectName>(InvalidLength);

        return new ProjectName(value);
    }

    public static ProjectName FromValue(string value)
    {
        return new ProjectName(value);
    }
}