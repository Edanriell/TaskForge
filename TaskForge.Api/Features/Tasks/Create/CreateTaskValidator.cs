using FluentValidation;
using Microsoft.IdentityModel.Tokens;

namespace TaskForge.Api.Features.Tasks.Create;

public sealed class CreateTaskValidator : AbstractValidator<CreateTaskCommand> 
{
    public CreateTaskValidator()
    {
        RuleFor(task => task.ProjectId)
            .NotEmpty()
            .WithMessage("Project id cannot be empty");
        
        RuleFor(task => task.Title)
            .NotEmpty()
            .WithMessage("Task title is required")
            .MaximumLength(48)
            .WithMessage("Task title must not exceed 48 characters");
    }
}