using MediatR;
using Microsoft.EntityFrameworkCore;
using ScoreExec.Application.Common.Interfaces;
using ScoreExec.Domain.Common;

namespace ScoreExec.Application.Features.Tournaments.Queries.GetTournamentProfile;

public record GetTournamentProfileQuery(Guid TournamentId) : IRequest<Result<TournamentProfileDto>>;

public class GetTournamentProfileQueryHandler : IRequestHandler<GetTournamentProfileQuery, Result<TournamentProfileDto>>
{
    private readonly IApplicationDbContext _context;

    public GetTournamentProfileQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<TournamentProfileDto>> Handle(GetTournamentProfileQuery request, CancellationToken cancellationToken)
    {
        var tournament = await _context.Tournaments
            .Include(t => t.Teams)
            .FirstOrDefaultAsync(t => t.Id == request.TournamentId, cancellationToken);

        if (tournament == null) return Result<TournamentProfileDto>.Failure("Tournament not found.", 404);

        var dto = new TournamentProfileDto
        {
            Id = tournament.Id,
            Name = tournament.Name,
            Description = tournament.Description,
            StartDate = tournament.StartDate,
            EndDate = tournament.EndDate,
            Teams = tournament.Teams.Select(t => new TournamentTeamDto
            {
                Id = t.Id,
                Name = t.Name
            }).ToList()
        };

        var matches = await _context.Matches
            .Include(m => m.HomeTeam)
            .Include(m => m.AwayTeam)
            .Where(m => m.TournamentId == tournament.Id)
            .OrderBy(m => m.ScheduledAt)
            .ToListAsync(cancellationToken);

        dto.Matches = matches.Select(m => new TournamentMatchDto
        {
            Id = m.Id,
            TeamA = m.HomeTeam.Name,
            TeamB = m.AwayTeam.Name,
            Date = m.ScheduledAt,
            Status = m.Status.ToString()
        }).ToList();

        return Result<TournamentProfileDto>.Success(dto);
    }
}
