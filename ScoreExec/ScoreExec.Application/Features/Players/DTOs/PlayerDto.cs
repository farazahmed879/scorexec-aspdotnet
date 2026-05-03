using ScoreExec.Domain.Enums;

namespace ScoreExec.Application.Features.Players.DTOs;

public record PlayerDto(
    Guid Id,
    Guid TenantId,
    string FirstName,
    string LastName,
    string FullName,
    int? JerseyNumber,
    BattingStyle BattingStyle,
    BowlingStyle BowlingStyle,
    bool IsActive,
    DateTime CreatedAt);
