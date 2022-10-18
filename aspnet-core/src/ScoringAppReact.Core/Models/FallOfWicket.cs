using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations.Schema;

namespace ScoringAppReact.Models
{
    public class FallOfWicket : FullAuditedEntity<long>, IMayHaveTenant
    {
        public long MatchId { get; set; }
        public long TeamId { get; set; }
        public int WicketNo { get; set; }
        public int Runs { get; set; }
        public long PlayerId { get; set; }
        public int? StartTime { get; set; }
        public int? EndTime { get; set; }

        [ForeignKey("TeamId")]
        public Team Team { get; set; }
        [ForeignKey("MatchId")]
        public Match Match { get; set; }
        public Player Player { get; set; }
        public int? TenantId { get; set; }
    }
}