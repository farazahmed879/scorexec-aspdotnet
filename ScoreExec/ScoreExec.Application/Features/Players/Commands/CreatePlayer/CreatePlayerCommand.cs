using MediatR;
using ScoreExec.Application.Common.Interfaces;
using ScoreExec.Application.Features.Players.DTOs;
using ScoreExec.Domain.Common;
using ScoreExec.Domain.Enums;

namespace ScoreExec.Application.Features.Players.Commands.CreatePlayer;

public record CreatePlayerCommand(
    string FirstName,
    string LastName,
    BattingStyle BattingStyle,
    BowlingStyle BowlingStyle,
    int? JerseyNumber = null,
    List<Guid>? TeamIds = null) : IRequest<Result<PlayerDto>>;
