using MediatR;
using ScoreExec.Domain.Common;

namespace ScoreExec.Application.Features.Tournaments.Commands.DeleteTournament;

public record DeleteTournamentCommand(Guid Id) : IRequest<Result<bool>>;
