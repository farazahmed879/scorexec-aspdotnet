namespace ScoreExec.Application.Features.Leaderboard.Queries.GetLeaderboard;

public class LeaderboardDto
{
    public Guid PlayerId { get; set; }
    public string PlayerName { get; set; } = string.Empty;
    public int Value { get; set; }
    public int Matches { get; set; }
    public int Innings { get; set; }
}
