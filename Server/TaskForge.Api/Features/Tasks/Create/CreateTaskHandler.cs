using MediatR;
using TaskForge.Api.Domain.Abstractions;
using TaskForge.Api.Domain.Projects;
using TaskForge.Api.Domain.Tasks;
using Task = TaskForge.Api.Domain.Tasks.Task;

namespace TaskForge.Api.Features.Tasks.Create;

public sealed class CreateTaskHandler(
    IProjectRepository projectRepository,
    IUnitOfWork unitOfWork)
    : IRequestHandler<CreateTaskCommand, Result<CreateTaskResponse>>
{
    public async Task<Result<CreateTaskResponse>> Handle(
        CreateTaskCommand request,
        CancellationToken cancellationToken = default)
    {
        var project = await projectRepository.GetByIdWithTasksAsync(request.ProjectId, cancellationToken);
        if (project is null)
        {
            return Result.Failure<CreateTaskResponse>(ProjectErrors.NotFound);
        }

        var taskTitleResult = TaskTitle.Create(request.Title);
        if (taskTitleResult.IsFailure)
        {
            return Result.Failure<CreateTaskResponse>(taskTitleResult.Error);
        }
        
        var taskCreationResult = Task.Create(project.Id, taskTitleResult.Value);

        var taskAdditionToProjectResult = project.AddTask(taskCreationResult.Value);
        if (taskAdditionToProjectResult.IsFailure)
        {
            return Result.Failure<CreateTaskResponse>(taskAdditionToProjectResult.Error);
        }

        try
        {
            await unitOfWork.BeginTransactionAsync(cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            await unitOfWork.CommitTransactionAsync(cancellationToken);
            
            return Result.Success(new CreateTaskResponse(project.Id, taskCreationResult.Value.Id));
        }
        catch
        {
            await unitOfWork.RollbackTransactionAsync(cancellationToken);
            throw;
        }

    }

}