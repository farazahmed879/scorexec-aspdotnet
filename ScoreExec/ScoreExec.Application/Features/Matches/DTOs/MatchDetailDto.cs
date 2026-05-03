using ScoreExec.Domain.Enums;

namespace ScoreExec.Application.Features.Matches.DTOs;

public record MatchDetailDto(
    Guid Id,
    Guid? TournamentId,
    Guid HomeTeamId,
    string HomeTeamName,
    Guid AwayTeamId,
    string AwayTeamName,
    DateTime ScheduledAt,
    string Venue,
    MatchStatus Status,
    int TotalOvers,
    Guid? WinnerTeamId,
    Guid? TossWinnerId,
    TossDecision? TossDecision,
    DateTime CreatedAt,
    List<InningsDto> Innings);

public record InningsDto(
    Guid Id,
    Guid MatchId,
    Guid BattingTeamId,
    string BattingTeamName,
    Guid BowlingTeamId,
    string BowlingTeamName,
    int InningsNumber,
    int TotalRuns,
    int TotalWickets,
    decimal TotalOvers,
    bool IsCompleted,
    List<OverDto> Overs);

public record OverDto(
    Guid Id,
    Guid InningsId,
    int OverNumber,
    Guid BowlerId,
    string BowlerName,
    bool IsCompleted,
    int TotalRuns,
    int Wickets,
    List<DeliveryDto> Deliveries);

public record DeliveryDto(
    Guid Id,
    Guid OverId,
    int BallNumber,
    Guid BatsmanId,
    string BatsmanName,
    Guid BowlerId,
    string BowlerName,
    int RunsScored,
    bool IsWide,
    bool IsNoBall,
    bool IsBye,
    bool IsLegBye,
    bool IsWicket,
    WicketType WicketType,
    Guid? WicketFielderId);

public record MatchListDto(
    Guid Id,
    Guid? TournamentId,
    Guid HomeTeamId,
    string HomeTeamName,
    Guid AwayTeamId,
    string AwayTeamName,
    DateTime ScheduledAt,
    string Venue,
    MatchStatus Status,
    int TotalOvers,
    DateTime CreatedAt);
