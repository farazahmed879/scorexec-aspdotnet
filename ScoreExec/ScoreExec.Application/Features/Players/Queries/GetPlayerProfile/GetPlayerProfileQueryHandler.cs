using MediatR;
using Microsoft.EntityFrameworkCore;
using ScoreExec.Application.Common.Interfaces;
using ScoreExec.Domain.Common;
using ScoreExec.Domain.Entities;
using ScoreExec.Domain.Enums;

namespace ScoreExec.Application.Features.Players.Queries.GetPlayerProfile;

public class GetPlayerProfileQueryHandler : IRequestHandler<GetPlayerProfileQuery, Result<PlayerProfileDto>>
{
    private readonly IApplicationDbContext _context;

    public GetPlayerProfileQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<PlayerProfileDto>> Handle(GetPlayerProfileQuery request, CancellationToken cancellationToken)
    {
        var player = await _context.Players
            .Include(p => p.Teams)
            .FirstOrDefaultAsync(p => p.Id == request.PlayerId, cancellationToken);

        if (player == null) return Result<PlayerProfileDto>.Failure("Player not found.", 404);

        // Base delivery queries for stats
        var batsmanQuery = _context.Deliveries.Where(d => d.BatsmanId == request.PlayerId);
        var bowlerQuery = _context.Deliveries.Where(d => d.BowlerId == request.PlayerId);
        var fielderQuery = _context.Deliveries.Where(d => d.WicketFielderId == request.PlayerId);

        // Apply filters to stats queries
        if (request.TournamentId.HasValue)
        {
            batsmanQuery = batsmanQuery.Where(d => d.Over.Innings.Match.TournamentId == request.TournamentId);
            bowlerQuery = bowlerQuery.Where(d => d.Over.Innings.Match.TournamentId == request.TournamentId);
            fielderQuery = fielderQuery.Where(d => d.Over.Innings.Match.TournamentId == request.TournamentId);
        }

        if (request.TeamId.HasValue)
        {
            batsmanQuery = batsmanQuery.Where(d => d.Over.Innings.Match.HomeTeamId == request.TeamId || d.Over.Innings.Match.AwayTeamId == request.TeamId);
            bowlerQuery = bowlerQuery.Where(d => d.Over.Innings.Match.HomeTeamId == request.TeamId || d.Over.Innings.Match.AwayTeamId == request.TeamId);
            fielderQuery = fielderQuery.Where(d => d.Over.Innings.Match.HomeTeamId == request.TeamId || d.Over.Innings.Match.AwayTeamId == request.TeamId);
        }

        if (request.StartDate.HasValue)
        {
            batsmanQuery = batsmanQuery.Where(d => d.Over.Innings.Match.ScheduledAt >= request.StartDate);
            bowlerQuery = bowlerQuery.Where(d => d.Over.Innings.Match.ScheduledAt >= request.StartDate);
            fielderQuery = fielderQuery.Where(d => d.Over.Innings.Match.ScheduledAt >= request.StartDate);
        }

        if (request.EndDate.HasValue)
        {
            batsmanQuery = batsmanQuery.Where(d => d.Over.Innings.Match.ScheduledAt <= request.EndDate);
            bowlerQuery = bowlerQuery.Where(d => d.Over.Innings.Match.ScheduledAt <= request.EndDate);
            fielderQuery = fielderQuery.Where(d => d.Over.Innings.Match.ScheduledAt <= request.EndDate);
        }

        var deliveriesAsBatsman = await batsmanQuery.ToListAsync(cancellationToken);
        var deliveriesAsBowler = await bowlerQuery.ToListAsync(cancellationToken);
        var deliveriesAsFielder = await fielderQuery.ToListAsync(cancellationToken);

        var dto = new PlayerProfileDto
        {
            Id = player.Id,
            FirstName = player.FirstName,
            LastName = player.LastName,
            FullName = player.FullName,
            JerseyNumber = player.JerseyNumber,
            BattingStyle = player.BattingStyle,
            BowlingStyle = player.BowlingStyle,
            IsActive = player.IsActive
        };

        // Batting Stats
        var battingInnings = deliveriesAsBatsman
            .GroupBy(d => d.Over.InningsId)
            .Select(g => new {
                Runs = g.Sum(d => d.RunsScored),
                Balls = g.Count(d => !d.IsWide)
            })
            .ToList();

        dto.BattingStats = new BattingStatsDto
        {
            Runs = battingInnings.Sum(i => i.Runs),
            BallsFaced = battingInnings.Sum(i => i.Balls),
            Fours = deliveriesAsBatsman.Count(d => d.RunsScored == 4),
            Sixes = deliveriesAsBatsman.Count(d => d.RunsScored == 6),
            Innings = battingInnings.Count,
            HighScore = battingInnings.Any() ? battingInnings.Max(i => i.Runs) : 0,
            Hundreds = battingInnings.Count(i => i.Runs >= 100),
            Fifties = battingInnings.Count(i => i.Runs >= 50 && i.Runs < 100),
            BestScore = battingInnings.Any() ? battingInnings.Max(i => i.Runs) : 0
        };
        
        if (dto.BattingStats.BallsFaced > 0)
        {
            dto.BattingStats.StrikeRate = Math.Round((double)dto.BattingStats.Runs / dto.BattingStats.BallsFaced * 100, 2);
        }

        // Bowling Stats
        var bowlingInnings = deliveriesAsBowler
            .GroupBy(d => d.Over.InningsId)
            .Select(g => new {
                Wickets = g.Count(d => d.IsWicket && d.WicketType != WicketType.RunOut),
                Runs = g.Sum(d => d.RunsScored + (d.IsWide || d.IsNoBall ? 1 : 0)),
                Balls = g.Count(d => !d.IsWide && !d.IsNoBall)
            })
            .ToList();

        var totalWickets = bowlingInnings.Sum(i => i.Wickets);
        var totalRunsConceded = bowlingInnings.Sum(i => i.Runs);
        var totalValidBalls = bowlingInnings.Sum(i => i.Balls);
        
        dto.BowlingStats = new BowlingStatsDto
        {
            Wickets = totalWickets,
            RunsConceded = totalRunsConceded,
            Overs = Math.Floor((decimal)totalValidBalls / 6) + (decimal)(totalValidBalls % 6) / 10,
            Innings = bowlingInnings.Count,
            FiveWicketHauls = bowlingInnings.Count(i => i.Wickets >= 5)
        };

        if (totalValidBalls > 0)
        {
            dto.BowlingStats.Economy = Math.Round((double)totalRunsConceded / (totalValidBalls / 6.0), 2);
        }

        if (totalWickets > 0)
        {
            dto.BowlingStats.Average = Math.Round((double)totalRunsConceded / totalWickets, 2);
        }

        // Fielding Stats
        dto.FieldingStats = new FieldingStatsDto
        {
            Catches = deliveriesAsFielder.Count(d => d.WicketType == WicketType.Caught),
            Stumpings = deliveriesAsFielder.Count(d => d.WicketType == WicketType.Stumped),
            RunOuts = deliveriesAsFielder.Count(d => d.WicketType == WicketType.RunOut)
        };

        // Played Teams (All History)
        dto.PlayedTeams = player.Teams.Select(t => new TeamHistoryDto
        {
            Id = t.Id,
            Name = t.Name
        }).ToList();

        // Fetch ALL related matches and tournaments for history sections
        var allInningsIds = await _context.Deliveries
            .Where(d => d.BatsmanId == request.PlayerId || d.BowlerId == request.PlayerId)
            .Select(d => d.Over.InningsId)
            .Distinct()
            .ToListAsync(cancellationToken);

        var matchInnings = await _context.Innings
            .Include(i => i.Match)
            .ThenInclude(m => m.HomeTeam)
            .Include(i => i.Match)
            .ThenInclude(m => m.AwayTeam)
            .Include(i => i.Match)
            .ThenInclude(m => m.Tournament)
            .Where(i => allInningsIds.Contains(i.Id))
            .ToListAsync(cancellationToken);

        var matches = matchInnings.Select(i => i.Match).DistinctBy(m => m.Id).ToList();

        dto.PlayedMatches = matches.Select(m => new MatchHistoryDto
        {
            Id = m.Id,
            OpponentTeam = $"{m.HomeTeam.Name} vs {m.AwayTeam.Name}",
            Date = m.ScheduledAt,
            Result = m.Status.ToString()
        }).ToList();

        dto.PlayedTournaments = matches
            .Where(m => m.Tournament != null)
            .Select(m => m.Tournament!)
            .DistinctBy(t => t.Id)
            .Select(t => new TournamentHistoryDto
            {
                Id = t.Id,
                Name = t.Name
            }).ToList();

        return Result<PlayerProfileDto>.Success(dto);
    }
}
