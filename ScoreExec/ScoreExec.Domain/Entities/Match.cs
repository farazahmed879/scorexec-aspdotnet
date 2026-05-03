using ScoreExec.Domain.Common;
using ScoreExec.Domain.Enums;

namespace ScoreExec.Domain.Entities;

public class Match : BaseEntity
{
    public Guid? TournamentId { get; private set; }
    public Guid HomeTeamId { get; private set; }
    public Guid AwayTeamId { get; private set; }
    public DateTime ScheduledAt { get; private set; }
    public string Venue { get; private set; } = string.Empty;
    public MatchStatus Status { get; private set; } = MatchStatus.Scheduled;
    public Guid? WinnerTeamId { get; private set; }
    public Guid? TossWinnerId { get; private set; }
    public TossDecision? TossDecision { get; private set; }
    public int TotalOvers { get; private set; }

    public Tournament? Tournament { get; private set; }
    public Team HomeTeam { get; private set; } = null!;
    public Team AwayTeam { get; private set; } = null!;
    public ICollection<Innings> Innings { get; private set; } = new List<Innings>();

    private Match()
    {
    }

    public static Match Create(
        Guid? tournamentId,
        Guid homeTeamId,
        Guid awayTeamId,
        DateTime scheduledAt,
        string venue,
        int totalOvers)
    {
        return new Match
        {
            TournamentId = tournamentId,
            HomeTeamId = homeTeamId,
            AwayTeamId = awayTeamId,
            ScheduledAt = scheduledAt,
            Venue = venue,
            TotalOvers = totalOvers,
            Status = MatchStatus.Scheduled
        };
    }

    public void SetToss(Guid tossWinnerId, TossDecision decision)
    {
        TossWinnerId = tossWinnerId;
        TossDecision = decision;
        MarkAsUpdated();
    }

    public void Start()
    {
        Status = MatchStatus.Live;
        MarkAsUpdated();
    }

    public void Complete(Guid winnerTeamId)
    {
        Status = MatchStatus.Completed;
        WinnerTeamId = winnerTeamId;
        MarkAsUpdated();
    }

    public void Abandon()
    {
        Status = MatchStatus.Abandoned;
        MarkAsUpdated();
    }
}
