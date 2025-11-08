using MediatR;
using TaskForge.Api.Domain.Abstractions;
using TaskForge.Api.Domain.Projects;

namespace TaskForge.Api.Features.Projects.Update;

public sealed class UpdateProjectHandler(
    IProjectRepository projectRepository,
    IUnitOfWork unitOfWork)
    : IRequestHandler<UpdateProjectCommand, Result<UpdateProjectResponse>>
{
    public async Task<Result<UpdateProjectResponse>> Handle(
        UpdateProjectCommand request,
        CancellationToken cancellationToken = default)
    {
        var project = await projectRepository.GetByIdWithTasksAsync(request.Id, cancellationToken);
        if (project is null)
        {
            return Result.Failure<UpdateProjectResponse>(ProjectErrors.NotFound);
        }
        
        var projectNameResult = ProjectName.Create(request.Name);
        if (projectNameResult.IsFailure) return Result.Failure<UpdateProjectResponse>(projectNameResult.Error);

        var projectDescriptionResult = ProjectDescription.Create(request.Description);
        if (projectDescriptionResult.IsFailure)
            return Result.Failure<UpdateProjectResponse>(projectDescriptionResult.Error);

        var projectUpdateResult = project.Update(projectNameResult.Value, projectDescriptionResult.Value);
        if (projectUpdateResult.IsFailure)
        {
            return Result.Failure<UpdateProjectResponse>(projectUpdateResult.Error);
        }
        
        try
        {
            await unitOfWork.BeginTransactionAsync(cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            await unitOfWork.CommitTransactionAsync(cancellationToken); 
            
            return Result.Success(new UpdateProjectResponse(project.Id));
        }
        catch
        {
            await unitOfWork.RollbackTransactionAsync(cancellationToken);
            throw;
        }
    }
}