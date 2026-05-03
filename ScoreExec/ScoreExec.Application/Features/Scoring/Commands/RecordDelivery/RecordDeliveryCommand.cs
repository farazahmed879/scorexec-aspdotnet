using MediatR;
using ScoreExec.Domain.Common;
using ScoreExec.Domain.Enums;

namespace ScoreExec.Application.Features.Scoring.Commands.RecordDelivery;

public record RecordDeliveryCommand(
    Guid InningsId,
    Guid OverId,
    int BallNumber,
    Guid BatsmanId,
    Guid BowlerId,
    int RunsScored,
    bool IsWide = false,
    bool IsNoBall = false,
    bool IsBye = false,
    bool IsLegBye = false,
    bool IsWicket = false,
    WicketType WicketType = WicketType.None,
    Guid? WicketFielderId = null) : IRequest<Result>;
