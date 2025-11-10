using System.Text;
using Dapper;
using MediatR;
using TaskForge.Api.Domain.Abstractions;
using TaskForge.Api.Infrastructure.Data;

namespace TaskForge.Api.Features.Projects.GetAll;

public sealed class GetAllProjectsHandler(
    ISqlConnectionFactory sqlConnectionFactory) : IRequestHandler<GetAllProjectsQuery, Result<GetAllProjectsResponse>>
{
    public async Task<Result<GetAllProjectsResponse>> Handle(
        GetAllProjectsQuery request,
        CancellationToken cancellationToken)
    {
        using var connection = sqlConnectionFactory.CreateConnection();

        var sqlBuilder = new StringBuilder();

        sqlBuilder.Append("""
                          SELECT
                            project.id as Id,
                            project.name as Name,
                            project.description as Description,
                            project.created_at as CreatedAt,
                            
                            task.id as Id,
                            task.title as Title,
                            task.status as Status
                          FROM projects project
                          LEFT JOIN tasks task ON project.id = task.project_id
                          ORDER BY project.created_at DESC
                          """);

        var projectDictionary = new Dictionary<Guid, ProjectResponse>();

        await connection.QueryAsync<ProjectResponse, ProjectTaskResponse, ProjectResponse>(
            sqlBuilder.ToString(),
            (project, task) =>
            {
                if (!projectDictionary.TryGetValue(project.Id, out var existingProject))
                {
                    // Create tasks list
                    IReadOnlyList<ProjectTaskResponse> tasks = task != null && task.Id != Guid.Empty
                        ? new List<ProjectTaskResponse> { task }
                        : new List<ProjectTaskResponse>();

                    var newProject = project with
                    {
                        Tasks = tasks
                    };
                    projectDictionary.Add(project.Id, newProject);
                    return newProject;
                }

                // Add task to existing project if not already added
                if (task != null &&
                    task.Id != Guid.Empty &&
                    existingProject.Tasks != null &&
                    !existingProject.Tasks.Any(t => t.Id == task.Id))
                {
                    var updatedTasks = existingProject.Tasks.ToList();
                    updatedTasks.Add(task);

                    var updatedProject = existingProject with { Tasks = updatedTasks };
                    projectDictionary[project.Id] = updatedProject;
                }

                return existingProject;
            },
            splitOn: "Id");

        var projects = projectDictionary.Values.ToList();

        return new GetAllProjectsResponse
        {
            Projects = projects
        };
    }
}