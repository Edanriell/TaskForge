using System.Text;
using Dapper;
using MediatR;
using TaskForge.Api.Domain.Abstractions;
using TaskForge.Api.Domain.Projects;
using TaskForge.Api.Infrastructure.Data;

namespace TaskForge.Api.Features.Projects.GetById;

internal sealed class GetProjectByIdHandler : IRequestHandler<GetProjectByIdQuery, Result<GetProjectByIdResponse>>
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public GetProjectByIdHandler(ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }

    public async Task<Result<GetProjectByIdResponse>> Handle(
        GetProjectByIdQuery request,
        CancellationToken cancellationToken)
    {
        using var connection = _sqlConnectionFactory.CreateConnection();

        var sqlBuilder = new StringBuilder();

        sqlBuilder.Append("""
                          SELECT
                              project.id AS Id,
                              project.name AS Name,
                              project.description AS Description,
                              project.created_at AS CreatedAt,

                              task.id AS TaskId,
                              task.title AS Title,
                              task.status AS Status
                          FROM projects project
                          LEFT JOIN tasks task ON project.id = task.project_id
                          WHERE p.id = @ProjectId
                          """);

        var projectDictionary = new Dictionary<Guid, ProjectResponse>();

        await connection.QueryAsync<ProjectResponse, ProjectTaskResponse, ProjectResponse>(
            sqlBuilder.ToString(),
            (project, task) =>
            {
                if (!projectDictionary.TryGetValue(project.Id, out var existingProject))
                {
                    var tasks = new List<ProjectTaskResponse>();

                    if (task is not null && task.Id != Guid.Empty) tasks.Add(task);

                    var newProject = project with { Tasks = tasks };
                    projectDictionary.Add(project.Id, newProject);
                    return newProject;
                }

                // Add task if not already present
                if (task is not null &&
                    task.Id != Guid.Empty &&
                    !existingProject.Tasks.Any(t => t.Id == task.Id))
                {
                    var updatedTasks = existingProject.Tasks.ToList();
                    updatedTasks.Add(task);

                    existingProject = existingProject with { Tasks = updatedTasks };
                    projectDictionary[project.Id] = existingProject;
                }

                return existingProject;
            },
            new { request.ProjectId },
            splitOn: "TaskId"
        );

        var project = projectDictionary.Values.FirstOrDefault();

        if (project is null) return Result.Failure<GetProjectByIdResponse>(ProjectErrors.NotFound);

        return Result.Success(new GetProjectByIdResponse
        {
            Project = project
        });
    }
}