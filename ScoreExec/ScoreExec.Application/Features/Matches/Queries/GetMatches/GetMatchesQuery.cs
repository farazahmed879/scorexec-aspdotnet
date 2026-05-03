using MediatR;
using Microsoft.EntityFrameworkCore;
using ScoreExec.Application.Common.Interfaces;
using ScoreExec.Application.Features.Matches.DTOs;
using ScoreExec.Domain.Common;

namespace ScoreExec.Application.Features.Matches.Queries.GetMatches;

public record GetMatchesQuery(Guid? TournamentId = null) : IRequest<Result<List<MatchListDto>>>;

public class GetMatchesQueryHandler : IRequestHandler<GetMatchesQuery, Result<List<MatchListDto>>>
{
    private readonly IApplicationDbContext _context;

    public GetMatchesQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<List<MatchListDto>>> Handle(GetMatchesQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Matches
            .AsNoTracking()
            .Include(m => m.HomeTeam)
            .Include(m => m.AwayTeam)
            .AsQueryable();

        if (request.TournamentId.HasValue)
        {
            query = query.Where(m => m.TournamentId == request.TournamentId.Value);
        }

        var matches = await query
            .OrderByDescending(m => m.CreatedAt)
            .Select(m => new MatchListDto(
                m.Id,
                m.TournamentId,
                m.HomeTeamId,
                m.HomeTeam.Name,
                m.AwayTeamId,
                m.AwayTeam.Name,
                m.ScheduledAt,
                m.Venue,
                m.Status,
                m.TotalOvers,
                m.CreatedAt))
            .ToListAsync(cancellationToken);

        return Result<List<MatchListDto>>.Success(matches);
    }
}
