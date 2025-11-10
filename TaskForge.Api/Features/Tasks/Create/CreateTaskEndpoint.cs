using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace TaskForge.Api.Features.Tasks.Create;

[ApiController]
[Route("api/projects/{projectId:guid}/tasks")]
public class CreateTaskEndpoint(IMediator mediator) : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(CreateTaskResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateTask(
        [FromRoute] Guid projectId,
        [FromBody] CreateTaskCommand command,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new CreateTaskCommand(projectId, command.Title), cancellationToken);

        if (result.IsFailure)
            return BadRequest(new
            {
                error = result.Error.Code,
                message = result.Error.Name
            });
        
        return CreatedAtAction(
            nameof(CreateTask),
            new { id = result.Value },
            result.Value);
    }

}