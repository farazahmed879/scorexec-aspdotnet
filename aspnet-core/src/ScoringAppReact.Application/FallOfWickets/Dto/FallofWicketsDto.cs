using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Abp.Authorization.Roles;
using ScoringAppReact.Authorization.Roles;

namespace ScoringAppReact.FallOfWickets.Dto
{
    public class FallofWicketsDto
    {
        public long Id { get; set; }
        public long MatchId { get; set; }
        public long TeamId { get; set; }
        public int WicketNo { get; set; }
        public int Runs { get; set; }
        public long PlayerId { get; set; }
        public int? StartTime { get; set; }
        public int? EndTime { get; set; }
        public int? TenantId { get; set; }
    }
}
