using TaskForge.Api.Domain.Abstractions;

namespace TaskForge.Api.Domain.Projects;

public sealed record ProjectDescription
{
    private const int MaxProjectDescriptionLength = 264;

    public static readonly Error InvalidName = new(
        "ProjectDescription.InvalidName",
        "Project description cannot be empty or null"
    );

    public static readonly Error InvalidLength = new(
        "ProjectDescription.InvalidLength",
        $"Project description cannot exceed {MaxProjectDescriptionLength} characters"
    );

    private ProjectDescription(string value)
    {
        Value = value;
    }

    public string Value { get; init; }

    public static Result<ProjectDescription> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value)) return Result.Failure<ProjectDescription>(InvalidName);

        if (value.Length > MaxProjectDescriptionLength) return Result.Failure<ProjectDescription>(InvalidLength);

        return new ProjectDescription(value);
    }

    public static ProjectDescription FromValue(string value)
    {
        return new ProjectDescription(value);
    }
}