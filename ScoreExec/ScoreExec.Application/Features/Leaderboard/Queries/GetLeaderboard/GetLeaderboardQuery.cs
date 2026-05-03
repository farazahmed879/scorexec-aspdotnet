using MediatR;
using Microsoft.EntityFrameworkCore;
using ScoreExec.Application.Common.Interfaces;
using ScoreExec.Domain.Common;

namespace ScoreExec.Application.Features.Leaderboard.Queries.GetLeaderboard;

public record GetLeaderboardQuery(string Type, Guid? TournamentId = null) : IRequest<Result<List<LeaderboardDto>>>;

public class GetLeaderboardQueryHandler : IRequestHandler<GetLeaderboardQuery, Result<List<LeaderboardDto>>>
{
    private readonly IApplicationDbContext _context;

    public GetLeaderboardQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<List<LeaderboardDto>>> Handle(GetLeaderboardQuery request, CancellationToken cancellationToken)
    {
        var deliveriesQuery = _context.Deliveries.AsNoTracking();
        
        if (request.TournamentId.HasValue)
        {
            deliveriesQuery = deliveriesQuery.Where(d => d.Over.Innings.Match.TournamentId == request.TournamentId);
        }

        List<LeaderboardDto> result = new();

        switch (request.Type)
        {
            case "MostRuns":
                result = await deliveriesQuery
                    .GroupBy(d => new { d.BatsmanId, d.Batsman.FirstName, d.Batsman.LastName })
                    .Select(g => new LeaderboardDto
                    {
                        PlayerId = g.Key.BatsmanId,
                        PlayerName = g.Key.FirstName + " " + g.Key.LastName,
                        Value = g.Sum(d => d.RunsScored)
                    })
                    .OrderByDescending(x => x.Value)
                    .Take(10)
                    .ToListAsync(cancellationToken);
                break;

            case "MostWickets":
                result = await deliveriesQuery
                    .Where(d => d.IsWicket && d.WicketType != ScoreExec.Domain.Enums.WicketType.RunOut)
                    .GroupBy(d => new { d.BowlerId, d.Bowler.FirstName, d.Bowler.LastName })
                    .Select(g => new LeaderboardDto
                    {
                        PlayerId = g.Key.BowlerId,
                        PlayerName = g.Key.FirstName + " " + g.Key.LastName,
                        Value = g.Count()
                    })
                    .OrderByDescending(x => x.Value)
                    .Take(10)
                    .ToListAsync(cancellationToken);
                break;

            case "MostSixes":
                result = await deliveriesQuery
                    .Where(d => d.RunsScored == 6 && !d.IsNoBall && !d.IsWide)
                    .GroupBy(d => new { d.BatsmanId, d.Batsman.FirstName, d.Batsman.LastName })
                    .Select(g => new LeaderboardDto
                    {
                        PlayerId = g.Key.BatsmanId,
                        PlayerName = g.Key.FirstName + " " + g.Key.LastName,
                        Value = g.Count()
                    })
                    .OrderByDescending(x => x.Value)
                    .Take(10)
                    .ToListAsync(cancellationToken);
                break;

            case "MostFours":
                result = await deliveriesQuery
                    .Where(d => d.RunsScored == 4 && !d.IsNoBall && !d.IsWide)
                    .GroupBy(d => new { d.BatsmanId, d.Batsman.FirstName, d.Batsman.LastName })
                    .Select(g => new LeaderboardDto
                    {
                        PlayerId = g.Key.BatsmanId,
                        PlayerName = g.Key.FirstName + " " + g.Key.LastName,
                        Value = g.Count()
                    })
                    .OrderByDescending(x => x.Value)
                    .Take(10)
                    .ToListAsync(cancellationToken);
                break;
        }

        // Add matches and innings counts (approximate for now)
        foreach (var item in result)
        {
            item.Matches = await _context.Deliveries
                .Where(d => d.BatsmanId == item.PlayerId || d.BowlerId == item.PlayerId)
                .Select(d => d.Over.Innings.MatchId)
                .Distinct()
                .CountAsync(cancellationToken);
        }

        return Result<List<LeaderboardDto>>.Success(result);
    }
}
