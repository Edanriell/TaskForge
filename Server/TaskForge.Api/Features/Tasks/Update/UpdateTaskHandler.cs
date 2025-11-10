using MediatR;
using TaskForge.Api.Domain.Abstractions;
using TaskForge.Api.Domain.Projects;
using TaskForge.Api.Domain.Tasks;
using TaskForge.Api.Features.Projects.Update;

namespace TaskForge.Api.Features.Tasks.Update;

public sealed class UpdateTaskHandler(
    IProjectRepository projectRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<UpdateTaskCommand, Result<UpdateTaskResponse>>
{
    public async Task<Result<UpdateTaskResponse>> Handle(
        UpdateTaskCommand request,
        CancellationToken cancellationToken = default)
    {
        var project = await projectRepository.GetByIdWithTasksAsync(request.ProjectId, cancellationToken);
        if (project is null)
        {
            return Result.Failure<UpdateTaskResponse>(ProjectErrors.NotFound);
        }

        var taskTitleResult = TaskTitle.Create(request.Title);
        if (taskTitleResult.IsFailure)
        {
            return Result.Failure<UpdateTaskResponse>(taskTitleResult.Error);
        }

        var updateTaskResult = project.UpdateTask(request.Id, taskTitleResult.Value, request.TaskStatus);
        if (updateTaskResult.IsFailure)
        {
            return Result.Failure<UpdateTaskResponse>(updateTaskResult.Error);
        }

        try 
        {
            await unitOfWork.BeginTransactionAsync(cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            await unitOfWork.CommitTransactionAsync(cancellationToken);

            return Result.Success(new UpdateTaskResponse(request.ProjectId, request.Id));
        }
        catch
        {
            await unitOfWork.RollbackTransactionAsync(cancellationToken);
            throw;
        }
    }
}