using FluentValidation;

namespace TaskForge.Api.Features.Projects.Create;

public sealed class CreateProjectValidator : AbstractValidator<CreateProjectCommand>
{
    public CreateProjectValidator()
    {
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