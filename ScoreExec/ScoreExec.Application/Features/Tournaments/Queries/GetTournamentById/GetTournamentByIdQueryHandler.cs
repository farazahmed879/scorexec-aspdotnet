using MediatR;
using Microsoft.EntityFrameworkCore;
using ScoreExec.Application.Common.Interfaces;
using ScoreExec.Application.Features.Tournaments.DTOs;
using ScoreExec.Domain.Common;

namespace ScoreExec.Application.Features.Tournaments.Queries.GetTournamentById;

public class GetTournamentByIdQueryHandler : IRequestHandler<GetTournamentByIdQuery, Result<TournamentDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUser;

    public GetTournamentByIdQueryHandler(IApplicationDbContext context, ICurrentUserService currentUser)
    {
        _context = context;
        _currentUser = currentUser;
    }

    public async Task<Result<TournamentDto>> Handle(GetTournamentByIdQuery request, CancellationToken cancellationToken)
    {
        var tournament = await _context.Tournaments
            .Where(t => t.Id == request.Id && t.TenantId == _currentUser.TenantId)
            .Select(t => new TournamentDto(
                t.Id,
                t.TenantId,
                t.Name,
                t.Description,
                t.Sport,
                t.Format,
                t.Type,
                t.LogoUrl,
                t.StartDate,
                t.EndDate,
                t.Status,
                t.MaxTeams,
                t.CreatedAt))
            .FirstOrDefaultAsync(cancellationToken);

        if (tournament is null)
        {
            return Result<TournamentDto>.NotFound("Tournament not found.");
        }

        return Result<TournamentDto>.Success(tournament);
    }
}
