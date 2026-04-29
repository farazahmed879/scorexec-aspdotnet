using Microsoft.AspNetCore.Identity;
using ScoreExec.Domain.Enums;

namespace ScoreExec.Infrastructure.Persistence;

public class AppUser : IdentityUser<Guid>
{
    public Guid TenantId { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public UserRole Role { get; set; } = UserRole.Viewer;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public bool IsDeleted { get; set; }

    public string FullName => $"{FirstName} {LastName}";
}
