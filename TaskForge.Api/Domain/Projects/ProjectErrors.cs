using TaskForge.Api.Domain.Abstractions;

namespace TaskForge.Api.Domain.Projects;

public static class ProjectErrors
{
    public static readonly Error NoChanges = new(
        "Project.NoChanges",
        "No changes were made to the project");

    public static readonly Error NotFound = new(
        "Project.NotFound",
        "The specified project with provided id could not be found");
}