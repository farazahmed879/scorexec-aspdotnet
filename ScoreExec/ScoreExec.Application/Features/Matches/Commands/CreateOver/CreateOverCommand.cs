using MediatR;
using Microsoft.EntityFrameworkCore;
using ScoreExec.Application.Common.Interfaces;
using ScoreExec.Domain.Common;
using ScoreExec.Domain.Entities;

namespace ScoreExec.Application.Features.Matches.Commands.CreateOver;

public record CreateOverCommand(
    Guid InningsId,
    int OverNumber,
    Guid BowlerId) : IRequest<Result>;

public class CreateOverCommandHandler : IRequestHandler<CreateOverCommand, Result>
{
    private readonly IApplicationDbContext _context;

    public CreateOverCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result> Handle(CreateOverCommand request, CancellationToken cancellationToken)
    {
        var innings = await _context.Innings
            .FirstOrDefaultAsync(i => i.Id == request.InningsId, cancellationToken);

        if (innings == null) return Result.Failure("Innings not found.", 404);

        if (innings.IsCompleted)
            return Result.Failure("Innings is already completed.");

        var over = Over.Create(request.InningsId, request.OverNumber, request.BowlerId);
        await _context.Overs.AddAsync(over, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
