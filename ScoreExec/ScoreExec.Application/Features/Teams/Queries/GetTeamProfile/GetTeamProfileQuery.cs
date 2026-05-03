using MediatR;
using Microsoft.EntityFrameworkCore;
using ScoreExec.Application.Common.Interfaces;
using ScoreExec.Application.Features.Players.DTOs;
using ScoreExec.Domain.Common;

namespace ScoreExec.Application.Features.Teams.Queries.GetTeamProfile;

public record GetTeamProfileQuery(Guid TeamId) : IRequest<Result<TeamProfileDto>>;

public class GetTeamProfileQueryHandler : IRequestHandler<GetTeamProfileQuery, Result<TeamProfileDto>>
{
    private readonly IApplicationDbContext _context;

    public GetTeamProfileQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<TeamProfileDto>> Handle(GetTeamProfileQuery request, CancellationToken cancellationToken)
    {
        var team = await _context.Teams
            .Include(t => t.Players)
            .FirstOrDefaultAsync(t => t.Id == request.TeamId, cancellationToken);

        if (team == null) return Result<TeamProfileDto>.Failure("Team not found.", 404);

        var dto = new TeamProfileDto
        {
            Id = team.Id,
            Name = team.Name,
            HomeColor = team.HomeColor,
            AwayColor = team.AwayColor,
            Players = team.Players.Select(p => new PlayerDto(
                p.Id, p.TenantId, p.FirstName, p.LastName, p.FullName, 
                p.JerseyNumber, p.BattingStyle, p.BowlingStyle, p.IsActive, p.CreatedAt
            )).ToList()
        };

        // Fetch recent matches
        var matches = await _context.Matches
            .Include(m => m.HomeTeam)
            .Include(m => m.AwayTeam)
            .Where(m => m.HomeTeamId == team.Id || m.AwayTeamId == team.Id)
            .OrderByDescending(m => m.ScheduledAt)
            .Take(10)
            .ToListAsync(cancellationToken);

        dto.RecentMatches = matches.Select(m => new TeamMatchDto
        {
            Id = m.Id,
            OpponentName = m.HomeTeamId == team.Id ? m.AwayTeam.Name : m.HomeTeam.Name,
            Date = m.ScheduledAt,
            Result = m.Status.ToString()
        }).ToList();

        return Result<TeamProfileDto>.Success(dto);
    }
}
