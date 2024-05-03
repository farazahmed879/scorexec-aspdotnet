using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Abp.Authorization;
using ScoringAppReact.Authorization.Roles;
using Abp.Domain.Uow; // Add this namespace for IUnitOfWorkManager
using Abp.Authorization.Roles;
using Abp.Authorization.Users;

namespace ScoringAppReact.Authorization.Users
{
    public class UserClaimsPrincipalFactory : AbpUserClaimsPrincipalFactory<User, Role>
    {
        public UserClaimsPrincipalFactory(
            AbpUserManager<Role, User> userManager, // Change to AbpUserManager
            AbpRoleManager<Role, User> roleManager, // Change to AbpRoleManager
            IOptions<IdentityOptions> optionsAccessor,
            IUnitOfWorkManager unitOfWorkManager) // Add this parameter
            : base(
                  userManager,
                  roleManager,
                  optionsAccessor,
                  unitOfWorkManager) // Pass unitOfWorkManager to the base constructor
        {
        }
    }
}
