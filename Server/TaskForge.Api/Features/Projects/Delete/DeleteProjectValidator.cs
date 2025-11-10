using FluentValidation;

namespace TaskForge.Api.Features.Projects.Delete;

public sealed class DeleteProjectValidator : AbstractValidator<DeleteProjectCommand>
{
    public DeleteProjectValidator()
    {
        RuleFor(project => project.Id)
            .NotEmpty()
            .WithMessage("Project id cannot be empty");
    }
}