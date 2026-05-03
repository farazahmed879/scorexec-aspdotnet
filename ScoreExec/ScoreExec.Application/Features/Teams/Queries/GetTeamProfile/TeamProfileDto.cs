using ScoreExec.Application.Features.Players.DTOs;

namespace ScoreExec.Application.Features.Teams.Queries.GetTeamProfile;

public class TeamProfileDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? HomeColor { get; set; }
    public string? AwayColor { get; set; }
    public List<PlayerDto> Players { get; set; } = new();
    public List<TeamMatchDto> RecentMatches { get; set; } = new();
}

public class TeamMatchDto
{
    public Guid Id { get; set; }
    public string OpponentName { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public string Result { get; set; } = string.Empty;
}
