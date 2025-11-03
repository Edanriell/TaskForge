using MediatR;
using TaskForge.Api.Domain.Abstractions;

namespace TaskForge.Api.Features.Projects.GetById;

public sealed record GetProjectByIdQuery(Guid ProjectId) : IRequest<Result<GetProjectByIdResponse>>;