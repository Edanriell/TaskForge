using MediatR;
using TaskForge.Api.Domain.Abstractions;
using TaskForge.Api.Domain.Projects;

namespace TaskForge.Api.Features.Projects.Delete;

public sealed class DeleteProjectHandler(
    IProjectRepository projectRepository,
    IUnitOfWork unitOfWork)
    : IRequestHandler<DeleteProjectCommand, Result>
{
    public async Task<Result> Handle(
        DeleteProjectCommand request,
        CancellationToken cancellationToken = default)
    {
        var project = await projectRepository.GetByIdWithTasksAsync(request.Id, cancellationToken);
        if (project is null) return Result.Failure<Result>(ProjectErrors.NotFound);

        var projectDeletionResult = project.Delete();
        if (projectDeletionResult.IsFailure) return Result.Failure<Result>(projectDeletionResult.Error);

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