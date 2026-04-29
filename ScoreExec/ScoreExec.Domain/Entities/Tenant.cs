using ScoreExec.Domain.Common;
using ScoreExec.Domain.Enums;

namespace ScoreExec.Domain.Entities;

public class Tenant : BaseEntity
{
    public string Name { get; private set; } = string.Empty;
    public string Slug { get; private set; } = string.Empty;
    public bool IsActive { get; private set; } = true;
    public SubscriptionPlan SubscriptionPlan { get; private set; } = SubscriptionPlan.Free;

    public ICollection<Tournament> Tournaments { get; private set; } = new List<Tournament>();

    private Tenant()
    {
    }

    public static Tenant Create(string name, string slug, SubscriptionPlan plan = SubscriptionPlan.Free)
    {
        return new Tenant
        {
            Name = name,
            Slug = slug.ToLower().Trim(),
            SubscriptionPlan = plan,
            IsActive = true
        };
    }

    public void Update(string name, SubscriptionPlan plan)
    {
        Name = name;
        SubscriptionPlan = plan;
        MarkAsUpdated();
    }

    public void Deactivate()
    {
        IsActive = false;
        MarkAsUpdated();
    }
}
