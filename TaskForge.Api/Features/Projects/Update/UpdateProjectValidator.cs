using FluentValidation;

namespace TaskForge.Api.Features.Projects.Update;

public sealed class UpdateProjectValidator : AbstractValidator<UpdateProjectCommand>
{
    public UpdateProjectValidator()
    {
        RuleFor(project => project.Id)
            .NotEmpty()
            .WithMessage("Project id cannot be empty");
        
        RuleFor(project => project.Name)
            .NotEmpty()
            .WithMessage("Project name is required")
            .MaximumLength(64)
            .WithMessage("Project name must not exceed 64 characters");

        RuleFor(project => project.Description)
            .NotEmpty()
            .WithMessage("Project description is required")
            .MaximumLength(264)
            .WithMessage("Project description must not exceed 264 characters");
    }
} 