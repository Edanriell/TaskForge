using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace TaskForge.Api.Features.Tasks.Update;

[ApiController]
[Route("api/projects/{projectId:guid}/tasks/{taskId:guid}")]
public sealed class UpdateTaskEndpoint(IMediator mediator) : ControllerBase
{
    [HttpPatch]
    [ProducesResponseType(typeof(UpdateTaskResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateTask(
        [FromRoute] Guid projectId,
        [FromRoute] Guid taskId,
        [FromBody] UpdateTaskCommand command,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(
            new UpdateTaskCommand(projectId, taskId, command.Title, command.TaskStatus));

        if (result.IsFailure)
            return BadRequest(new
            {
                error = result.Error.Code,
                message = result.Error.Name
            });
        
        return Ok(result.Value);
    }
}

// PATCH /api/projects/{id}/tasks/{taskId}