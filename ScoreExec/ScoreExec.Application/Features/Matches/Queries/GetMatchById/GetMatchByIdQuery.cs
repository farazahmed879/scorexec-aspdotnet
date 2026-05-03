using MediatR;
using Microsoft.EntityFrameworkCore;
using ScoreExec.Application.Common.Interfaces;
using ScoreExec.Application.Features.Matches.DTOs;
using ScoreExec.Domain.Common;

namespace ScoreExec.Application.Features.Matches.Queries.GetMatchById;

public record GetMatchByIdQuery(Guid MatchId) : IRequest<Result<MatchDetailDto>>;

public class GetMatchByIdQueryHandler : IRequestHandler<GetMatchByIdQuery, Result<MatchDetailDto>>
{
    private readonly IApplicationDbContext _context;

    public GetMatchByIdQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<MatchDetailDto>> Handle(GetMatchByIdQuery request, CancellationToken cancellationToken)
    {
        var match = await _context.Matches
            .AsNoTracking()
            .Include(m => m.HomeTeam)
            .Include(m => m.AwayTeam)
            .Include(m => m.Innings)
                .ThenInclude(i => i.Overs)
                    .ThenInclude(o => o.Deliveries)
                        .ThenInclude(d => d.Batsman)
            .Include(m => m.Innings)
                .ThenInclude(i => i.Overs)
                    .ThenInclude(o => o.Bowler)
            .FirstOrDefaultAsync(m => m.Id == request.MatchId, cancellationToken);

        if (match == null)
            return Result<MatchDetailDto>.NotFound("Match not found.");

        // We need bowler names for deliveries too - they come from the Over's bowler
        var dto = new MatchDetailDto(
            match.Id,
            match.TournamentId,
            match.HomeTeamId,
            match.HomeTeam.Name,
            match.AwayTeamId,
            match.AwayTeam.Name,
            match.ScheduledAt,
            match.Venue,
            match.Status,
            match.TotalOvers,
            match.WinnerTeamId,
            match.TossWinnerId,
            match.TossDecision,
            match.CreatedAt,
            match.Innings.OrderBy(i => i.InningsNumber).Select(i =>
            {
                // Resolve team names
                string battingTeamName = i.BattingTeamId == match.HomeTeamId ? match.HomeTeam.Name : match.AwayTeam.Name;
                string bowlingTeamName = i.BowlingTeamId == match.HomeTeamId ? match.HomeTeam.Name : match.AwayTeam.Name;

                return new InningsDto(
                    i.Id,
                    i.MatchId,
                    i.BattingTeamId,
                    battingTeamName,
                    i.BowlingTeamId,
                    bowlingTeamName,
                    i.InningsNumber,
                    i.TotalRuns,
                    i.TotalWickets,
                    i.TotalOvers,
                    i.IsCompleted,
                    i.Overs.OrderBy(o => o.OverNumber).Select(o => new OverDto(
                        o.Id,
                        o.InningsId,
                        o.OverNumber,
                        o.BowlerId,
                        o.Bowler?.FullName ?? "Unknown",
                        o.IsCompleted,
                        o.TotalRuns,
                        o.Wickets,
                        o.Deliveries.OrderBy(d => d.BallNumber).Select(d => new DeliveryDto(
                            d.Id,
                            d.OverId,
                            d.BallNumber,
                            d.BatsmanId,
                            d.Batsman?.FullName ?? "Unknown",
                            d.BowlerId,
                            o.Bowler?.FullName ?? "Unknown",
                            d.RunsScored,
                            d.IsWide,
                            d.IsNoBall,
                            d.IsBye,
                            d.IsLegBye,
                            d.IsWicket,
                            d.WicketType,
                            d.WicketFielderId
                        )).ToList()
                    )).ToList()
                );
            }).ToList()
        );

        return Result<MatchDetailDto>.Success(dto);
    }
}
