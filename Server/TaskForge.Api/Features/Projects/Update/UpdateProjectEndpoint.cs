using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaskForge.Api.Features.Projects.GetById;

namespace TaskForge.Api.Features.Projects.Update;

[ApiController]
[Route("api/projects/{id:guid}")]
public sealed class UpdateProjectEndpoint(IMediator mediator) : ControllerBase
{
    [HttpPut]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateProject(
        [FromRoute] Guid id,
        [FromBody] UpdateProjectCommand command,
        CancellationToken cancellationToken)
    { 
        var result = await mediator.Send(
            new UpdateProjectCommand(id, command.Name, command.Description),
            cancellationToken);

        if (result.IsFailure)
            return BadRequest(new
            {
                error = result.Error.Code,
                message = result.Error.Name
            });

        return Ok(result.Value);
    }
}