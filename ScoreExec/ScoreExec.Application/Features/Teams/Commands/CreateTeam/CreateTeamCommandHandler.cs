using MediatR;
using ScoreExec.Application.Common.Interfaces;
using ScoreExec.Application.Features.Teams.DTOs;
using ScoreExec.Domain.Common;
using ScoreExec.Domain.Entities;

namespace ScoreExec.Application.Features.Teams.Commands.CreateTeam;

public class CreateTeamCommandHandler : IRequestHandler<CreateTeamCommand, Result<TeamDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUser;

    public CreateTeamCommandHandler(IApplicationDbContext context, ICurrentUserService currentUser)
    {
        _context = context;
        _currentUser = currentUser;
    }

    public async Task<Result<TeamDto>> Handle(CreateTeamCommand request, CancellationToken cancellationToken)
    {
        var team = Team.Create(
            _currentUser.TenantId,
            request.TournamentId,
            request.Name,
            request.LogoUrl,
            request.HomeColor,
            request.AwayColor);

        await _context.Teams.AddAsync(team, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        var dto = new TeamDto(
            team.Id,
            team.TenantId,
            team.TournamentId,
            team.Name,
            team.LogoUrl,
            team.HomeColor,
            team.AwayColor,
            team.CreatedAt);

        return Result<TeamDto>.Created(dto);
    }
}
