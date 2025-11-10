using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace TaskForge.Api.Features.Tasks.Delete;

[ApiController]
[Route("api/projects/{projectId:guid}/tasks/{id:guid}")]
public class DeleteTaskEndpoint(IMediator mediator) : ControllerBase
{
    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteProject(
        [FromRoute] DeleteTaskCommand command,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(command, cancellationToken);

        if (result.IsFailure)
            return BadRequest(new
            {
                error = result.Error.Code,
                message = result.Error.Name,
            });
        
        return NoContent();
    }
}