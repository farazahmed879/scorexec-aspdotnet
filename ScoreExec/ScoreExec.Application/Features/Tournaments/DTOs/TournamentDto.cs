using ScoreExec.Domain.Enums;

namespace ScoreExec.Application.Features.Tournaments.DTOs;

public record TournamentDto(
    Guid Id,
    Guid TenantId,
    string Name,
    string? Description,
    Sport Sport,
    TournamentFormat Format,
    DateTime StartDate,
    DateTime EndDate,
    TournamentStatus Status,
    int MaxTeams,
    DateTime CreatedAt
);
