using MediatR;
using TaskForge.Api.Domain.Abstractions;

namespace TaskForge.Api.Features.Projects.Update;

public sealed record UpdateProjectCommand(Guid Id, string Name, string Description) : IRequest<Result<UpdateProjectResponse>>;