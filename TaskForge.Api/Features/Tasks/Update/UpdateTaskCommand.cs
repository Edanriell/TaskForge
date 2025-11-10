using MediatR;
using TaskForge.Api.Domain.Abstractions;

using TaskStatus = TaskForge.Api.Domain.Tasks.TaskStatus;

namespace TaskForge.Api.Features.Tasks.Update;

public sealed record UpdateTaskCommand(Guid ProjectId, Guid Id, string Title, TaskStatus TaskStatus) : IRequest<Result<UpdateTaskResponse>>;