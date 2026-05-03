using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScoreExec.Application.Features.Scoring.Commands.RecordDelivery;

namespace ScoreExec.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ScoringController : ControllerBase
{
    private readonly IMediator _mediator;

    public ScoringController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("delivery")]
    public async Task<IActionResult> RecordDelivery([FromBody] RecordDeliveryCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        return result.IsSuccess ? NoContent() : StatusCode(result.StatusCode, result.Error);
    }
}
