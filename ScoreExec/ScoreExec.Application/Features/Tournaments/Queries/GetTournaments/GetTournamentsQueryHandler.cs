using MediatR;
using Microsoft.EntityFrameworkCore;
using ScoreExec.Application.Common.Interfaces;
using ScoreExec.Application.Features.Tournaments.DTOs;
using ScoreExec.Domain.Common;

namespace ScoreExec.Application.Features.Tournaments.Queries.GetTournaments;

public class GetTournamentsQueryHandler : IRequestHandler<GetTournamentsQuery, Result<List<TournamentDto>>>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUser;

    public GetTournamentsQueryHandler(IApplicationDbContext context, ICurrentUserService currentUser)
    {
        _context = context;
        _currentUser = currentUser;
    }

    public async Task<Result<List<TournamentDto>>> Handle(GetTournamentsQuery request, CancellationToken cancellationToken)
    {
        var tournaments = await _context.Tournaments
            .Where(t => t.TenantId == _currentUser.TenantId)
            .OrderByDescending(t => t.CreatedAt)
            .Select(t => new TournamentDto(
                t.Id,
                t.TenantId,
                t.Name,
                t.Description,
                t.Sport,
                t.Format,
                t.StartDate,
                t.EndDate,
                t.Status,
                t.MaxTeams,
                t.CreatedAt))
            .ToListAsync(cancellationToken);

        return Result<List<TournamentDto>>.Success(tournaments);
    }
}
