using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScoreExec.Application.Features.Leaderboard.Queries.GetLeaderboard;

namespace ScoreExec.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class LeaderboardController : ControllerBase
{
    private readonly IMediator _mediator;

    public LeaderboardController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("most-runs")]
    public async Task<IActionResult> GetMostRuns([FromQuery] Guid? tournamentId, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetLeaderboardQuery("MostRuns", tournamentId), cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : StatusCode(result.StatusCode, result.Error);
    }

    [HttpGet("most-wickets")]
    public async Task<IActionResult> GetMostWickets([FromQuery] Guid? tournamentId, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetLeaderboardQuery("MostWickets", tournamentId), cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : StatusCode(result.StatusCode, result.Error);
    }

    [HttpGet("most-sixes")]
    public async Task<IActionResult> GetMostSixes([FromQuery] Guid? tournamentId, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetLeaderboardQuery("MostSixes", tournamentId), cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : StatusCode(result.StatusCode, result.Error);
    }

    [HttpGet("most-fours")]
    public async Task<IActionResult> GetMostFours([FromQuery] Guid? tournamentId, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetLeaderboardQuery("MostFours", tournamentId), cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : StatusCode(result.StatusCode, result.Error);
    }
}
