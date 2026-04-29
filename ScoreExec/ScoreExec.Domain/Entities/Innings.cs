using ScoreExec.Domain.Common;

namespace ScoreExec.Domain.Entities;

public class Innings : BaseEntity
{
    public Guid MatchId { get; private set; }
    public Guid BattingTeamId { get; private set; }
    public Guid BowlingTeamId { get; private set; }
    public int InningsNumber { get; private set; }
    public int TotalRuns { get; private set; }
    public int TotalWickets { get; private set; }
    public decimal TotalOvers { get; private set; }
    public bool IsCompleted { get; private set; }

    public Match Match { get; private set; } = null!;
    public ICollection<Over> Overs { get; private set; } = new List<Over>();

    private Innings()
    {
    }

    public static Innings Create(Guid matchId, Guid battingTeamId, Guid bowlingTeamId, int inningsNumber)
    {
        return new Innings
        {
            MatchId = matchId,
            BattingTeamId = battingTeamId,
            BowlingTeamId = bowlingTeamId,
            InningsNumber = inningsNumber,
            TotalRuns = 0,
            TotalWickets = 0,
            TotalOvers = 0,
            IsCompleted = false
        };
    }

    public void UpdateScore(int runs, int wickets, decimal overs)
    {
        TotalRuns = runs;
        TotalWickets = wickets;
        TotalOvers = overs;
        MarkAsUpdated();
    }

    public void Complete()
    {
        IsCompleted = true;
        MarkAsUpdated();
    }
}
