using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace TaskForge.Api.Features.Projects.GetById;

[ApiController]
[Route("api/projects/{id:guid}")]
public sealed class GetProjectByIdEndpoint(IMediator mediator) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(GetProjectByIdResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetAllProjects(
        [FromRoute] GetProjectByIdQuery query,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(query, cancellationToken);

        if (result.IsFailure)
            return BadRequest(new
            {
                error = result.Error.Code,
                message = result.Error.Name
            });

        return Ok(result);
    }
}