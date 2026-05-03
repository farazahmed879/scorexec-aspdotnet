using ScoreExec.Domain.Common;

namespace ScoreExec.Domain.Entities;

public class Team : BaseEntity
{
    public Guid TenantId { get; private set; }
    public Guid? TournamentId { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string? LogoUrl { get; private set; }
    public string? HomeColor { get; private set; }
    public string? AwayColor { get; private set; }

    public Tournament? Tournament { get; private set; }
    public ICollection<Player> Players { get; private set; } = new List<Player>();

    private Team()
    {
    }

    public static Team Create(
        Guid tenantId,
        Guid? tournamentId,
        string name,
        string? logoUrl = null,
        string? homeColor = null,
        string? awayColor = null)
    {
        return new Team
        {
            TenantId = tenantId,
            TournamentId = tournamentId,
            Name = name,
            LogoUrl = logoUrl,
            HomeColor = homeColor,
            AwayColor = awayColor
        };
    }

    public void Update(string name, string? logoUrl, string? homeColor, string? awayColor)
    {
        Name = name;
        LogoUrl = logoUrl;
        HomeColor = homeColor;
        AwayColor = awayColor;
        MarkAsUpdated();
    }
}
