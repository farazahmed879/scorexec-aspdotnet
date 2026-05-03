using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScoreExec.Application.Features.Matches.Commands.CreateMatch;
using ScoreExec.Application.Features.Matches.Commands.StartMatch;
using ScoreExec.Application.Features.Matches.Commands.CreateInnings;
using ScoreExec.Application.Features.Matches.Commands.CreateOver;
using ScoreExec.Application.Features.Matches.Queries.GetMatches;
using ScoreExec.Application.Features.Matches.Queries.GetMatchById;

namespace ScoreExec.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class MatchesController : ControllerBase
{
    private readonly IMediator _mediator;

    public MatchesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] Guid? tournamentId, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetMatchesQuery(tournamentId), cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : StatusCode(result.StatusCode, result.Error);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetMatchByIdQuery(id), cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : StatusCode(result.StatusCode, result.Error);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateMatchCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : StatusCode(result.StatusCode, result.Error);
    }

    [HttpPost("{id:guid}/start")]
    public async Task<IActionResult> StartMatch(Guid id, [FromBody] StartMatchRequest request, CancellationToken cancellationToken)
    {
        var command = new StartMatchCommand(id, request.TossWinnerId, request.TossDecision, request.OpeningBowlerId);
        var result = await _mediator.Send(command, cancellationToken);
        return result.IsSuccess ? NoContent() : StatusCode(result.StatusCode, result.Error);
    }

    [HttpPost("{id:guid}/innings")]
    public async Task<IActionResult> CreateInnings(Guid id, [FromBody] CreateInningsRequest request, CancellationToken cancellationToken)
    {
        var command = new CreateInningsCommand(id, request.BattingTeamId, request.BowlingTeamId, request.OpeningBowlerId);
        var result = await _mediator.Send(command, cancellationToken);
        return result.IsSuccess ? NoContent() : StatusCode(result.StatusCode, result.Error);
    }

    [HttpPost("{id:guid}/overs")]
    public async Task<IActionResult> CreateOver(Guid id, [FromBody] CreateOverRequest request, CancellationToken cancellationToken)
    {
        // We need InningsId, so the client must send it
        var command = new CreateOverCommand(request.InningsId, request.OverNumber, request.BowlerId);
        var result = await _mediator.Send(command, cancellationToken);
        return result.IsSuccess ? NoContent() : StatusCode(result.StatusCode, result.Error);
    }
}

// Request DTOs (to separate route params from body)
public record StartMatchRequest(Guid TossWinnerId, ScoreExec.Domain.Enums.TossDecision TossDecision, Guid OpeningBowlerId);
public record CreateInningsRequest(Guid BattingTeamId, Guid BowlingTeamId, Guid OpeningBowlerId);
public record CreateOverRequest(Guid InningsId, int OverNumber, Guid BowlerId);
