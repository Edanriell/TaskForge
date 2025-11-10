using FluentValidation;

namespace TaskForge.Api.Features.Tasks.Update;

public sealed class UpdateTaskValidator : AbstractValidator<UpdateTaskCommand>
{
    public UpdateTaskValidator()
    {
        RuleFor(task => task.ProjectId)
            .NotEmpty()
            .WithMessage("Project id cannot be empty");
        
        RuleFor(task => task.Id)
            .NotEmpty()
            .WithMessage("Task id cannot be empty");
        
        RuleFor(task => task.Title)
            .NotEmpty()
            .WithMessage("Task title is required")
            .MaximumLength(48)
            .WithMessage("Task title must not exceed 48 characters");
        
        RuleFor(task => task.TaskStatus)
            .IsInEnum()
            .WithMessage("Task status is invalid");
    }
}