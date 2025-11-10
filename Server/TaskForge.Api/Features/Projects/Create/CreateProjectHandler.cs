using MediatR;
using TaskForge.Api.Domain.Abstractions;
using TaskForge.Api.Domain.Projects;

namespace TaskForge.Api.Features.Projects.Create;

public sealed class CreateProjectHandler(
    IProjectRepository projectRepository,
    IUnitOfWork unitOfWork)
    : IRequestHandler<CreateProjectCommand, Result<CreateProjectResponse>>
{
    public async Task<Result<CreateProjectResponse>> Handle(
        CreateProjectCommand request,
        CancellationToken cancellationToken = default)
    {
        var projectNameResult = ProjectName.Create(request.Name);
        if (projectNameResult.IsFailure) return Result.Failure<CreateProjectResponse>(projectNameResult.Error);

        var projectDescriptionResult = ProjectDescription.Create(request.Description);
        if (projectDescriptionResult.IsFailure)
            return Result.Failure<CreateProjectResponse>(projectDescriptionResult.Error);

        var projectCreationResult = Project.Create(projectNameResult.Value, projectDescriptionResult.Value);

        projectRepository.Add(projectCreationResult.Value);

        try
        {
            await unitOfWork.BeginTransactionAsync(cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            await unitOfWork.CommitTransactionAsync(cancellationToken);

            return Result.Success(new CreateProjectResponse(projectCreationResult.Value.Id));
        }
        catch
        {
            await unitOfWork.RollbackTransactionAsync(cancellationToken);
            throw;
        }
    }
}