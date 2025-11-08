using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace TaskForge.Api.Features.Projects.Update;

// FromRoute and from body !!!!!
[ApiController]
[Route("api/projects/{id:guid}")]
public sealed class UpdateProjectEndpoint(IMediator mediator) : ControllerBase
{
    [HttpPut]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateProject(
        [FromBody] UpdateProjectCommand command,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(command, cancellationToken);

        if (result.IsFailure)
            return BadRequest(new
            {
                error = result.Error.Code,
                message = result.Error.Name
            });
        
        // THINK !
        return Ok(result.Value);
    }
}