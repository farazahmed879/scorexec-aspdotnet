using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ScoreExec.Domain.Enums;
using ScoreExec.Domain.Entities;
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

        // Seed Dummy Data
        if (!await context.Set<Tenant>().AnyAsync())
        {
            var tenant = Tenant.Create("ScoreExec Demo", "demo-tenant");
            await context.Set<Tenant>().AddAsync(tenant);
            await context.SaveChangesAsync();

            // Teams
            var team1 = Team.Create(tenant.Id, null, "Mumbai Mavericks", "https://img.freepik.com/free-vector/cricket-logo-template-design_23-2149021200.jpg", "#004ba0", "#ffffff");
            var team2 = Team.Create(tenant.Id, null, "Delhi Dynamos", "https://img.freepik.com/free-vector/cricket-club-logo-design_23-2149013000.jpg", "#e53935", "#ffffff");
            var team3 = Team.Create(tenant.Id, null, "Bangalore Blasters", "https://img.freepik.com/free-vector/cricket-badge-logo-vector_23-2149012999.jpg", "#000000", "#e53935");
            
            await context.Set<Team>().AddRangeAsync(team1, team2, team3);
            await context.SaveChangesAsync();

            // Players
            var player1 = Player.Create(tenant.Id, "Virat", "Kohli", BattingStyle.RightHand, BowlingStyle.RightArmFast, 18);
            player1.Teams.Add(team1);
            
            var player2 = Player.Create(tenant.Id, "Rohit", "Sharma", BattingStyle.RightHand, BowlingStyle.OffSpin, 45);
            player2.Teams.Add(team1);
            
            var player3 = Player.Create(tenant.Id, "MS", "Dhoni", BattingStyle.RightHand, BowlingStyle.None, 7);
            player3.Teams.Add(team2);

            var player4 = Player.Create(tenant.Id, "Jasprit", "Bumrah", BattingStyle.RightHand, BowlingStyle.RightArmFast, 93);
            player4.Teams.Add(team2);

            var player5 = Player.Create(tenant.Id, "Hardik", "Pandya", BattingStyle.RightHand, BowlingStyle.RightArmFast, 33);
            player5.Teams.Add(team3);
            
            await context.Set<Player>().AddRangeAsync(player1, player2, player3, player4, player5);
            await context.SaveChangesAsync();

            // Tournament
            var tournament = Tournament.Create(
                tenant.Id,
                "ScoreExec Premier League 2024",
                "The ultimate cricket championship featuring the best teams in the region. Join us for a month of high-octane action!",
                Sport.Cricket,
                TournamentFormat.T20,
                TournamentType.League,
                "https://img.freepik.com/free-vector/cricket-championship-banner-design_23-2149013002.jpg",
                DateTime.UtcNow.AddDays(7),
                DateTime.UtcNow.AddDays(30),
                10
            );
            await context.Set<Tournament>().AddAsync(tournament);
            await context.SaveChangesAsync();
        }
    }
}
