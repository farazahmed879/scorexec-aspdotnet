namespace ScoreExec.Application.Features.Tournaments.Queries.GetTournamentProfile;

public class TournamentProfileDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string? LogoUrl { get; set; }
    public string Type { get; set; } = string.Empty;
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public List<TournamentTeamDto> Teams { get; set; } = new();
    public List<TournamentMatchDto> Matches { get; set; } = new();
}

public class TournamentTeamDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
}

public class TournamentMatchDto
{
    public Guid Id { get; set; }
    public string TeamA { get; set; } = string.Empty;
    public string TeamB { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public string Status { get; set; } = string.Empty;
}
