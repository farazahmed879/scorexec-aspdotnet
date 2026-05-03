using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ScoreExec.Domain.Enums;
using ScoreExec.Infrastructure.Persistence;

namespace ScoreExec.Infrastructure;

public static class DbInitializer
{
    public static async Task SeedAsync(AppDbContext context, UserManager<AppUser> userManager, RoleManager<IdentityRole<Guid>> roleManager)
    {
        await context.Database.MigrateAsync();

        // Seed Super Admin
        var adminEmail = "admin@scoreexec.com";
        var adminUserName = "admin";
        
        var user = await userManager.FindByNameAsync(adminUserName);
        
        if (user == null)
        {
            user = new AppUser
            {
                UserName = adminUserName,
                Email = adminEmail,
                FirstName = "Super",
                LastName = "Admin",
                Role = UserRole.SuperAdmin,
                TenantId = Guid.Empty, // Super admin doesn't belong to a specific tenant
                CreatedAt = DateTime.UtcNow
            };
            
            await userManager.CreateAsync(user, "@Admin123");
        }
    }
}
