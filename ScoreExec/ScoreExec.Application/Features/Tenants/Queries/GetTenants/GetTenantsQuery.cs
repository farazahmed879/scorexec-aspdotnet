using MediatR;
using Microsoft.EntityFrameworkCore;
using ScoreExec.Application.Common.Interfaces;
using ScoreExec.Domain.Common;

namespace ScoreExec.Application.Features.Tenants.Queries.GetTenants;

public record GetTenantsQuery() : IRequest<Result<List<TenantDto>>>;

public record TenantDto(Guid Id, string Name, string Slug, bool IsActive);

public class GetTenantsQueryHandler : IRequestHandler<GetTenantsQuery, Result<List<TenantDto>>>
{
    private readonly IApplicationDbContext _context;

    public GetTenantsQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<List<TenantDto>>> Handle(GetTenantsQuery request, CancellationToken cancellationToken)
    {
        var tenants = await _context.Tenants
            .Select(t => new TenantDto(t.Id, t.Name, t.Slug, t.IsActive))
            .ToListAsync(cancellationToken);

        return Result<List<TenantDto>>.Success(tenants);
    }
}
