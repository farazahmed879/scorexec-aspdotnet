using ScoreExec.Domain.Common;
using ScoreExec.Domain.Enums;

namespace ScoreExec.Domain.Entities;

public class Player : BaseEntity
{
    public Guid TenantId { get; private set; }
    public Guid TeamId { get; private set; }
    public string FirstName { get; private set; } = string.Empty;
    public string LastName { get; private set; } = string.Empty;
    public int? JerseyNumber { get; private set; }
    public BattingStyle BattingStyle { get; private set; }
    public BowlingStyle BowlingStyle { get; private set; }
    public bool IsActive { get; private set; } = true;

    public string FullName => $"{FirstName} {LastName}";

    public Team Team { get; private set; } = null!;

    private Player()
    {
    }

    public static Player Create(
        Guid tenantId,
        Guid teamId,
        string firstName,
        string lastName,
        BattingStyle battingStyle,
        BowlingStyle bowlingStyle,
        int? jerseyNumber = null)
    {
        return new Player
        {
            TenantId = tenantId,
            TeamId = teamId,
            FirstName = firstName,
            LastName = lastName,
            BattingStyle = battingStyle,
            BowlingStyle = bowlingStyle,
            JerseyNumber = jerseyNumber,
            IsActive = true
        };
    }

    public void Update(
        string firstName,
        string lastName,
        BattingStyle battingStyle,
        BowlingStyle bowlingStyle,
        int? jerseyNumber)
    {
        FirstName = firstName;
        LastName = lastName;
        BattingStyle = battingStyle;
        BowlingStyle = bowlingStyle;
        JerseyNumber = jerseyNumber;
        MarkAsUpdated();
    }
}
