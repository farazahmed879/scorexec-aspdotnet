using MediatR;
using ScoreExec.Application.Features.Tournaments.DTOs;
using ScoreExec.Domain.Common;
using ScoreExec.Domain.Enums;

namespace ScoreExec.Application.Features.Tournaments.Commands.UpdateTournament;

public record UpdateTournamentCommand(
    Guid Id,
    string Name,
    string? Description,
    Sport Sport,
    TournamentFormat Format,
    DateTime StartDate,
    DateTime EndDate,
    int MaxTeams
) : IRequest<Result<TournamentDto>>;
