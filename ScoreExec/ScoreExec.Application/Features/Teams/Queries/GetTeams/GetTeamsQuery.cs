using MediatR;
using Microsoft.EntityFrameworkCore;
using ScoreExec.Application.Common.Interfaces;
using ScoreExec.Application.Features.Teams.DTOs;
using ScoreExec.Domain.Common;

namespace ScoreExec.Application.Features.Teams.Queries.GetTeams;

public record GetTeamsQuery(Guid? TournamentId = null) : IRequest<Result<List<TeamDto>>>;

public class GetTeamsQueryHandler : IRequestHandler<GetTeamsQuery, Result<List<TeamDto>>>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUser;

    public GetTeamsQueryHandler(IApplicationDbContext context, ICurrentUserService currentUser)
    {
        _context = context;
        _currentUser = currentUser;
    }

    public async Task<Result<List<TeamDto>>> Handle(GetTeamsQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Teams
            .AsNoTracking()
            .Where(t => t.TenantId == _currentUser.TenantId);

        if (request.TournamentId.HasValue)
        {
            query = query.Where(t => t.TournamentId == request.TournamentId.Value);
        }

        var teams = await query
            .Select(t => new TeamDto(
                t.Id,
                t.TenantId,
                t.TournamentId,
                t.Name,
                t.LogoUrl,
                t.HomeColor,
                t.AwayColor,
                t.CreatedAt))
            .ToListAsync(cancellationToken);

        return Result<List<TeamDto>>.Success(teams);
    }
}
