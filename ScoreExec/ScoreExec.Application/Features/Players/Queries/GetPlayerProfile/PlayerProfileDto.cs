using ScoreExec.Domain.Enums;

namespace ScoreExec.Application.Features.Players.Queries.GetPlayerProfile;

public class PlayerProfileDto
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public int? JerseyNumber { get; set; }
    public BattingStyle BattingStyle { get; set; }
    public BowlingStyle BowlingStyle { get; set; }
    public bool IsActive { get; set; }

    public BattingStatsDto BattingStats { get; set; } = new();
    public BowlingStatsDto BowlingStats { get; set; } = new();
    public FieldingStatsDto FieldingStats { get; set; } = new();

    public List<TeamHistoryDto> PlayedTeams { get; set; } = new();
    public List<MatchHistoryDto> PlayedMatches { get; set; } = new();
    public List<TournamentHistoryDto> PlayedTournaments { get; set; } = new();
}

public class BattingStatsDto
{
    public int Matches { get; set; }
    public int Innings { get; set; }
    public int Runs { get; set; }
    public int BallsFaced { get; set; }
    public int HighScore { get; set; }
    public double Average { get; set; }
    public double StrikeRate { get; set; }
    public int BestScore { get; set; }
    public int Hundreds { get; set; }
    public int Fifties { get; set; }
    public int Fours { get; set; }
    public int Sixes { get; set; }
    public int NotOuts { get; set; }
}

public class BowlingStatsDto
{
    public int Matches { get; set; }
    public int Innings { get; set; }
    public decimal Overs { get; set; }
    public int RunsConceded { get; set; }
    public int Wickets { get; set; }
    public double Economy { get; set; }
    public double Average { get; set; }
    public string BestBowling { get; set; } = string.Empty;
    public int FiveWicketHauls { get; set; }
}

public class FieldingStatsDto
{
    public int Catches { get; set; }
    public int Stumpings { get; set; }
    public int RunOuts { get; set; }
}

public class TeamHistoryDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
}

public class MatchHistoryDto
{
    public Guid Id { get; set; }
    public string OpponentTeam { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public string Result { get; set; } = string.Empty;
}

public class TournamentHistoryDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
}
