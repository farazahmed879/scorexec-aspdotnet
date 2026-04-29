using ScoreExec.Domain.Common;

namespace ScoreExec.Domain.Entities;

public class Over : BaseEntity
{
    public Guid InningsId { get; private set; }
    public int OverNumber { get; private set; }
    public Guid BowlerId { get; private set; }
    public bool IsCompleted { get; private set; }
    public int TotalRuns { get; private set; }
    public int Wickets { get; private set; }

    public Innings Innings { get; private set; } = null!;
    public Player Bowler { get; private set; } = null!;
    public ICollection<Delivery> Deliveries { get; private set; } = new List<Delivery>();

    private Over()
    {
    }

    public static Over Create(Guid inningsId, int overNumber, Guid bowlerId)
    {
        return new Over
        {
            InningsId = inningsId,
            OverNumber = overNumber,
            BowlerId = bowlerId,
            IsCompleted = false,
            TotalRuns = 0,
            Wickets = 0
        };
    }

    public void Complete()
    {
        IsCompleted = true;
        MarkAsUpdated();
    }

    public void UpdateStats(int totalRuns, int wickets)
    {
        TotalRuns = totalRuns;
        Wickets = wickets;
        MarkAsUpdated();
    }
}
