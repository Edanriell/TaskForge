using FluentValidation;

namespace TaskForge.Api.Features.Tasks.Delete;

public sealed class DeleteTaskValidator : AbstractValidator<DeleteTaskCommand>
{
    public DeleteTaskValidator()
    {
        RuleFor(task => task.ProjectId)
            .NotEmpty()
            .WithMessage("Project id cannot be empty");
        
        RuleFor(task => task.Id)
            .NotEmpty()
            .WithMessage("Task id cannot be empty");
    }
}