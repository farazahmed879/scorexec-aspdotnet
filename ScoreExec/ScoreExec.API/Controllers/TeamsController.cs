using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScoreExec.Application.Features.Teams.Commands.CreateTeam;
using ScoreExec.Application.Features.Teams.Queries.GetTeamProfile;
using ScoreExec.Application.Features.Teams.Queries.GetTeamPlayers;
using ScoreExec.Application.Features.Teams.Queries.GetTeams;

namespace ScoreExec.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class TeamsController : ControllerBase
{
    private readonly IMediator _mediator;

    public TeamsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] Guid? tournamentId, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetTeamsQuery(tournamentId), cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : StatusCode(result.StatusCode, result.Error);
    }

    [HttpGet("{id:guid}/profile")]
    public async Task<IActionResult> GetProfile(Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetTeamProfileQuery(id), cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : StatusCode(result.StatusCode, result.Error);
    }

    [HttpGet("{id:guid}/players")]
    public async Task<IActionResult> GetPlayers(Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetTeamPlayersQuery(id), cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : StatusCode(result.StatusCode, result.Error);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateTeamCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        return result.IsSuccess
            ? Ok(result.Value)
            : StatusCode(result.StatusCode, result.Error);
    }
}

