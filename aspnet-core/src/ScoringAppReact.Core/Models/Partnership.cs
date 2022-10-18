using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations.Schema;

namespace ScoringAppReact.Models
{
    public class Partnership : FullAuditedEntity<long>, IMayHaveTenant
    {
        public long MatchId { get; set; }
        public long TeamId { get; set; }
        public int WicketNo { get; set; }
        public long? PlayerOutId { get; set; }
        public int? StartTime { get; set; }
        public int? EndTime { get; set; }
        public int? Extras { get; set; }
        public int? Four { get; set; }
        public int? Six { get; set; }

        //player1
        public long Player1Id { get; set; }
        public int Player1Runs { get; set; }
        public int Player1Balls { get; set; }
        public int? Player1Six { get; set; }
        public int? Player1Four { get; set; }

        //player2
        public long Player2Id { get; set; }
        public long Player2Runs { get; set; }
        public long Player2Balls { get; set; }
        public int? Player2Six { get; set; }
        public int? Player2Four { get; set; }




        [ForeignKey("TeamId")]
        public Team Team { get; set; }
        [ForeignKey("MatchId")]
        public Match Match { get; set; }

        [ForeignKey("Player1Id")]
        public Player Player1 { get; set; }

        [ForeignKey("Player2Id")]
        public Player Player2 { get; set; }
        public int? TenantId { get; set; }
    }
}