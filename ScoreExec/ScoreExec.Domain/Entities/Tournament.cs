using ScoreExec.Domain.Common;
using ScoreExec.Domain.Enums;

namespace ScoreExec.Domain.Entities;

public class Tournament : BaseEntity
{
    public Guid TenantId { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string? Description { get; private set; }
    public Sport Sport { get; private set; }
    public TournamentFormat Format { get; private set; }
    public TournamentType Type { get; private set; }
    public string? LogoUrl { get; private set; }
    public DateTime StartDate { get; private set; }
    public DateTime EndDate { get; private set; }
    public TournamentStatus Status { get; private set; } = TournamentStatus.Upcoming;
    public int MaxTeams { get; private set; }

    public Tenant Tenant { get; private set; } = null!;
    public ICollection<Team> Teams { get; private set; } = new List<Team>();
    public ICollection<Match> Matches { get; private set; } = new List<Match>();

    private Tournament()
    {
    }

    public static Tournament Create(
        Guid tenantId,
        string name,
        string? description,
        Sport sport,
        TournamentFormat format,
        TournamentType type,
        string? logoUrl,
        DateTime startDate,
        DateTime endDate,
        int maxTeams)
    {
        return new Tournament
        {
            TenantId = tenantId,
            Name = name,
            Description = description,
            Sport = sport,
            Format = format,
            Type = type,
            LogoUrl = logoUrl,
            StartDate = startDate,
            EndDate = endDate,
            MaxTeams = maxTeams,
            Status = TournamentStatus.Upcoming
        };
    }

    public void Update(
        string name,
        string? description,
        Sport sport,
        TournamentFormat format,
        TournamentType type,
        string? logoUrl,
        DateTime startDate,
        DateTime endDate,
        int maxTeams)
    {
        Name = name;
        Description = description;
        Sport = sport;
        Format = format;
        Type = type;
        LogoUrl = logoUrl;
        StartDate = startDate;
        EndDate = endDate;
        MaxTeams = maxTeams;
        MarkAsUpdated();
    }

    public void UpdateStatus(TournamentStatus status)
    {
        Status = status;
        MarkAsUpdated();
    }
}
