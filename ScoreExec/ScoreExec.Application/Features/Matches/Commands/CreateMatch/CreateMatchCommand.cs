using MediatR;
using ScoreExec.Application.Common.Interfaces;
using ScoreExec.Application.Features.Matches.DTOs;
using ScoreExec.Domain.Common;

namespace ScoreExec.Application.Features.Matches.Commands.CreateMatch;

public record CreateMatchCommand(
    Guid HomeTeamId,
    Guid AwayTeamId,
    DateTime ScheduledAt,
    string Venue,
    int TotalOvers,
    Guid? TournamentId = null) : IRequest<Result<MatchDto>>;
