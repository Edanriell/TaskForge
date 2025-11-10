using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace TaskForge.Api.Features.Projects.GetAll;

[ApiController]
[Route("api/projects")]
public sealed class GetAllProjectsEndpoint(IMediator mediator) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(GetAllProjectsResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllProjects(
        [FromQuery] GetAllProjectsQuery query,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(query, cancellationToken);

        return Ok(result);
    }
}