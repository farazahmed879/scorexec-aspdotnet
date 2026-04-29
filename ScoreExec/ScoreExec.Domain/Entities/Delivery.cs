using ScoreExec.Domain.Common;
using ScoreExec.Domain.Enums;

namespace ScoreExec.Domain.Entities;

public class Delivery : BaseEntity
{
    public Guid OverId { get; private set; }
    public int BallNumber { get; private set; }
    public Guid BatsmanId { get; private set; }
    public Guid BowlerId { get; private set; }
    public int RunsScored { get; private set; }
    public bool IsWide { get; private set; }
    public bool IsNoBall { get; private set; }
    public bool IsBye { get; private set; }
    public bool IsLegBye { get; private set; }
    public bool IsWicket { get; private set; }
    public WicketType WicketType { get; private set; } = WicketType.None;
    public Guid? WicketFielderId { get; private set; }

    public Over Over { get; private set; } = null!;
    public Player Batsman { get; private set; } = null!;
    public Player Bowler { get; private set; } = null!;

    private Delivery()
    {
    }

    public static Delivery Create(
        Guid overId,
        int ballNumber,
        Guid batsmanId,
        Guid bowlerId,
        int runsScored,
        bool isWide = false,
        bool isNoBall = false,
        bool isBye = false,
        bool isLegBye = false,
        bool isWicket = false,
        WicketType wicketType = WicketType.None,
        Guid? wicketFielderId = null)
    {
        return new Delivery
        {
            OverId = overId,
            BallNumber = ballNumber,
            BatsmanId = batsmanId,
            BowlerId = bowlerId,
            RunsScored = runsScored,
            IsWide = isWide,
            IsNoBall = isNoBall,
            IsBye = isBye,
            IsLegBye = isLegBye,
            IsWicket = isWicket,
            WicketType = wicketType,
            WicketFielderId = wicketFielderId
        };
    }
}
