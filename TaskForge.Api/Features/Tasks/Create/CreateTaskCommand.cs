using MediatR;
using TaskForge.Api.Domain.Abstractions;

namespace TaskForge.Api.Features.Tasks.Create;

public sealed record CreateTaskCommand(Guid ProjectId, string Title) : IRequest<Result<CreateTaskResponse>>;

