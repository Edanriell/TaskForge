using MediatR;
using TaskForge.Api.Domain.Abstractions;

namespace TaskForge.Api.Features.Projects.Create;

public sealed record CreateProjectCommand(string Name, string Description) : IRequest<Result<CreateProjectResponse>>;