using MediatR;
using TaskForge.Api.Domain.Abstractions;

namespace TaskForge.Api.Features.Projects.Delete;

public sealed record DeleteProjectCommand(Guid Id) : IRequest<Result>;