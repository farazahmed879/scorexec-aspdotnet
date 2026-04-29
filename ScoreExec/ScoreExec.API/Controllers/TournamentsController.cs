using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScoreExec.Application.Features.Tournaments.Commands.CreateTournament;
using ScoreExec.Application.Features.Tournaments.Queries.GetTournamentById;
using ScoreExec.Application.Features.Tournaments.Queries.GetTournaments;

namespace ScoreExec.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class TournamentsController : ControllerBase
{
    private readonly IMediator _mediator;

    public TournamentsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetTournamentsQuery(), cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : StatusCode(result.StatusCode, result.Error);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetTournamentByIdQuery(id), cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : StatusCode(result.StatusCode, result.Error);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateTournamentCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        return result.IsSuccess
            ? CreatedAtAction(nameof(GetById), new { id = result.Value!.Id }, result.Value)
            : StatusCode(result.StatusCode, result.Error);
    }
}
