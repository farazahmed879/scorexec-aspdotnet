using MediatR;
using ScoreExec.Application.Common.Interfaces;
using ScoreExec.Application.Features.Teams.DTOs;
using ScoreExec.Domain.Common;

namespace ScoreExec.Application.Features.Teams.Commands.CreateTeam;

public record CreateTeamCommand(
    string Name,
    Guid? TournamentId = null,
    string? LogoUrl = null,
    string? HomeColor = null,
    string? AwayColor = null) : IRequest<Result<TeamDto>>;
