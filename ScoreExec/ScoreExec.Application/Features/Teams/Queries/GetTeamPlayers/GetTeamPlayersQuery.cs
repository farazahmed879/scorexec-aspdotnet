using MediatR;
using Microsoft.EntityFrameworkCore;
using ScoreExec.Application.Common.Interfaces;
using ScoreExec.Application.Features.Players.DTOs;
using ScoreExec.Domain.Common;

namespace ScoreExec.Application.Features.Teams.Queries.GetTeamPlayers;

public record GetTeamPlayersQuery(Guid TeamId) : IRequest<Result<List<PlayerDto>>>;

public class GetTeamPlayersQueryHandler : IRequestHandler<GetTeamPlayersQuery, Result<List<PlayerDto>>>
{
    private readonly IApplicationDbContext _context;

    public GetTeamPlayersQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<List<PlayerDto>>> Handle(GetTeamPlayersQuery request, CancellationToken cancellationToken)
    {
        var team = await _context.Teams
            .AsNoTracking()
            .Include(t => t.Players)
            .FirstOrDefaultAsync(t => t.Id == request.TeamId, cancellationToken);

        if (team == null)
            return Result<List<PlayerDto>>.NotFound("Team not found.");

        var players = team.Players.Select(p => new PlayerDto(
            p.Id,
            p.TenantId,
            p.FirstName,
            p.LastName,
            p.FullName,
            p.JerseyNumber,
            p.BattingStyle,
            p.BowlingStyle,
            p.IsActive,
            p.CreatedAt
        )).ToList();

        return Result<List<PlayerDto>>.Success(players);
    }
}
