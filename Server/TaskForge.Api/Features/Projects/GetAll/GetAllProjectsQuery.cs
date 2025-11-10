using MediatR;
using TaskForge.Api.Domain.Abstractions;

namespace TaskForge.Api.Features.Projects.GetAll;

public sealed record GetAllProjectsQuery : IRequest<Result<GetAllProjectsResponse>>;