using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Abp.Authorization;
using ScoringAppReact.Authorization.Roles;
using ScoringAppReact.Authorization.Users;
using ScoringAppReact.MultiTenancy;
using Microsoft.Extensions.Logging;
using Abp.Domain.Uow; // Add this namespace for IUnitOfWorkManager

namespace ScoringAppReact.Identity
{
    public class SecurityStampValidator : AbpSecurityStampValidator<Tenant, Role, User>
    {
        public SecurityStampValidator(
            IOptions<SecurityStampValidatorOptions> options,
            AbpSignInManager<Tenant, Role, User> signInManager, // Change to AbpSignInManager
            ISystemClock systemClock,
            ILoggerFactory loggerFactory,
            IUnitOfWorkManager unitOfWorkManager) // Add this parameter
            : base(options, signInManager, systemClock, loggerFactory, unitOfWorkManager)
        {
        }
    }
}
