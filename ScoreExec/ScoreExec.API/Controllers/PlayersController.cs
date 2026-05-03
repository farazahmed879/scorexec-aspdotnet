using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScoreExec.Application.Features.Players.Commands.AssignToTeam;
using ScoreExec.Application.Features.Players.Commands.CreatePlayer;
using ScoreExec.Application.Features.Players.Queries.GetPlayerProfile;
using ScoreExec.Application.Features.Players.Queries.GetPlayers;

namespace ScoreExec.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class PlayersController : ControllerBase
{
    private readonly IMediator _mediator;

    public PlayersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] Guid? teamId, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetPlayersQuery(teamId), cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : StatusCode(result.StatusCode, result.Error);
    }

    [HttpGet("{id:guid}/profile")]
    public async Task<IActionResult> GetProfile(
        Guid id, 
        [FromQuery] Guid? tournamentId, 
        [FromQuery] Guid? teamId, 
        [FromQuery] DateTime? startDate, 
        [FromQuery] DateTime? endDate, 
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetPlayerProfileQuery(id, tournamentId, teamId, startDate, endDate), cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : StatusCode(result.StatusCode, result.Error);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatePlayerCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : StatusCode(result.StatusCode, result.Error);
    }

    [HttpPost("{playerId:guid}/teams/{teamId:guid}")]
    public async Task<IActionResult> AssignToTeam(Guid playerId, Guid teamId, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new AssignPlayerToTeamCommand(playerId, teamId), cancellationToken);
        return result.IsSuccess ? NoContent() : StatusCode(result.StatusCode, result.Error);
    }
}
