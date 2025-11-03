using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace TaskForge.Api.Features.Projects.Delete;

[ApiController]
[Route("api/projects/{id:guid}")]
public sealed class DeleteProjectEndpoint(IMediator mediator) : ControllerBase
{
    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteProject(
        [FromQuery] DeleteProjectCommand command,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(command, cancellationToken);

        if (result.IsFailure)
            return BadRequest(new
            {
                error = result.Error.Code,
                message = result.Error.Name
            });

        return NoContent();
    }
}