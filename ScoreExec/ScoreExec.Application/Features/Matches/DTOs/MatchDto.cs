using ScoreExec.Domain.Enums;

namespace ScoreExec.Application.Features.Matches.DTOs;

public record MatchDto(
    Guid Id,
    Guid? TournamentId,
    Guid HomeTeamId,
    Guid AwayTeamId,
    DateTime ScheduledAt,
    string Venue,
    MatchStatus Status,
    int TotalOvers,
    Guid? WinnerTeamId,
    Guid? TossWinnerId,
    TossDecision? TossDecision,
    DateTime CreatedAt);
