using MediatR;
using Microsoft.EntityFrameworkCore;
using ScoreExec.Application.Common.Interfaces;
using ScoreExec.Application.Features.Players.DTOs;
using ScoreExec.Domain.Common;

namespace ScoreExec.Application.Features.Players.Queries.GetPlayers;

public record GetPlayersQuery(Guid? TeamId = null) : IRequest<Result<List<PlayerDto>>>;

public class GetPlayersQueryHandler : IRequestHandler<GetPlayersQuery, Result<List<PlayerDto>>>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUser;

    public GetPlayersQueryHandler(IApplicationDbContext context, ICurrentUserService currentUser)
    {
        _context = context;
        _currentUser = currentUser;
    }

    public async Task<Result<List<PlayerDto>>> Handle(GetPlayersQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Players
            .AsNoTracking()
            .Where(p => p.TenantId == _currentUser.TenantId);

        if (request.TeamId.HasValue)
        {
            query = query.Where(p => p.Teams.Any(t => t.Id == request.TeamId.Value));
        }

        var players = await query
            .Select(p => new PlayerDto(
                p.Id,
                p.TenantId,
                p.FirstName,
                p.LastName,
                p.FullName,
                p.JerseyNumber,
                p.BattingStyle,
                p.BowlingStyle,
                p.IsActive,
                p.CreatedAt))
            .ToListAsync(cancellationToken);

        return Result<List<PlayerDto>>.Success(players);
    }
}
