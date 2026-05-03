using MediatR;
using ScoreExec.Application.Common.Interfaces;
using ScoreExec.Domain.Common;
using ScoreExec.Domain.Entities;

namespace ScoreExec.Application.Features.Tenants.Commands.CreateTenant;

public class CreateTenantCommandHandler : IRequestHandler<CreateTenantCommand, Result<Guid>>
{
    private readonly IApplicationDbContext _context;

    public CreateTenantCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<Guid>> Handle(CreateTenantCommand request, CancellationToken cancellationToken)
    {
        var tenant = Tenant.Create(request.Name, request.Slug);
        _context.Tenants.Add(tenant);
        await _context.SaveChangesAsync(cancellationToken);

        return Result<Guid>.Success(tenant.Id);
    }
}
