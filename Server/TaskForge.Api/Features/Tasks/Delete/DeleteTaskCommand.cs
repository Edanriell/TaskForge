using MediatR;
using TaskForge.Api.Domain.Abstractions;

namespace TaskForge.Api.Features.Tasks.Delete;

public sealed record DeleteTaskCommand(Guid ProjectId, Guid Id) : IRequest<Result>;