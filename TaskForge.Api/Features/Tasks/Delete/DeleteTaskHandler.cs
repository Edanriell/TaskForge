using MediatR;
using TaskForge.Api.Domain.Abstractions;
using TaskForge.Api.Domain.Projects;

namespace TaskForge.Api.Features.Tasks.Delete;

public sealed class DeleteTaskHandler(
    IProjectRepository projectRepository,
    IUnitOfWork unitOfWork)
    : IRequestHandler<DeleteTaskCommand, Result>
{
    public async Task<Result> Handle(
        DeleteTaskCommand request,
        CancellationToken cancellationToken = default)
    {
        var project = await projectRepository.GetByIdWithTasksAsync(request.ProjectId, cancellationToken);
        if (project is null) return Result.Failure<Result>(ProjectErrors.NotFound);

        var taskDeletionResult = project.RemoveTask(request.Id);
        if (taskDeletionResult.IsFailure) return Result.Failure<Result>(taskDeletionResult.Error);

        try
        {
            await unitOfWork.BeginTransactionAsync(cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            await unitOfWork.CommitTransactionAsync(cancellationToken);

            return Result.Success();
        }
        catch
        {
            await unitOfWork.RollbackTransactionAsync(cancellationToken);
            throw;
        }
    }
}