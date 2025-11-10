using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace TaskForge.Api.Features.Projects.Create;

[ApiController]
[Route("api/projects")]
public sealed class CreateProjectEndpoint(IMediator mediator) : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateProject(
        [FromBody] CreateProjectCommand command,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(command, cancellationToken);

        if (result.IsFailure)
            return BadRequest(new
            {
                error = result.Error.Code,
                message = result.Error.Name
            });

        return CreatedAtAction(
            nameof(CreateProject),
            new { id = result.Value },
            result.Value);
    }
}

// POST /api/projects/{id}/tasks