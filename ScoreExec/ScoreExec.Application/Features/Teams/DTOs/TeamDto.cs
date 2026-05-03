namespace ScoreExec.Application.Features.Teams.DTOs;

public record TeamDto(
    Guid Id,
    Guid TenantId,
    Guid? TournamentId,
    string Name,
    string? LogoUrl,
    string? HomeColor,
    string? AwayColor,
    DateTime CreatedAt);
