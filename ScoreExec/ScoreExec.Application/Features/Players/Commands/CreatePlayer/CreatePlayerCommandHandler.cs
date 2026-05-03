using MediatR;
using Microsoft.EntityFrameworkCore;
using ScoreExec.Application.Common.Interfaces;
using ScoreExec.Application.Features.Players.DTOs;
using ScoreExec.Domain.Common;
using ScoreExec.Domain.Entities;

namespace ScoreExec.Application.Features.Players.Commands.CreatePlayer;

public class CreatePlayerCommandHandler : IRequestHandler<CreatePlayerCommand, Result<PlayerDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUser;

    public CreatePlayerCommandHandler(IApplicationDbContext context, ICurrentUserService currentUser)
    {
        _context = context;
        _currentUser = currentUser;
    }

    public async Task<Result<PlayerDto>> Handle(CreatePlayerCommand request, CancellationToken cancellationToken)
    {
        var player = Player.Create(
            _currentUser.TenantId,
            request.FirstName,
            request.LastName,
            request.BattingStyle,
            request.BowlingStyle,
            request.JerseyNumber);

        await _context.Players.AddAsync(player, cancellationToken);

        if (request.TeamIds != null && request.TeamIds.Any())
        {
            var teams = await _context.Teams
                .Where(t => request.TeamIds.Contains(t.Id))
                .ToListAsync(cancellationToken);

            foreach (var team in teams)
            {
                player.Teams.Add(team);
            }
        }

        await _context.SaveChangesAsync(cancellationToken);

        var dto = new PlayerDto(
            player.Id,
            player.TenantId,
            player.FirstName,
            player.LastName,
            player.FullName,
            player.JerseyNumber,
            player.BattingStyle,
            player.BowlingStyle,
            player.IsActive,
            player.CreatedAt);

        return Result<PlayerDto>.Created(dto);
    }
}
