using System;
using System.Collections.Generic;
using System.Text;

namespace ScoringAppReact.LiveScore.Dto
{
    public class PartnershipDto
    {
        public long MatchId { get; set; }
        public long TeamId { get; set; }
        public int WicketNo { get; set; }
        public int TotalRuns { get; set; }
        public int? StartTime { get; set; }
        public int? EndTime { get; set; }
        public int? Extras { get; set; }
        public int? Six { get; set; }
        public int? Four { get; set; }
        public long? PlayerOutId { get; set; }
        //player2
        public long Player1Id { get; set; }
        public string Player1Name { get; set; }
        public int Player1Runs { get; set; }
        public int? Player1Balls { get; set; }
        public int? Player1Six { get; set; }
        public int? Player1Four { get; set; }
        
        //player2
        public long Player2Id { get; set; }
        public string Player2Name { get; set; }
        public long Player2Runs { get; set; }
        public long Player2Balls { get; set; }
        public int? Player2Six { get; set; }
        public int? Player2Four { get; set; }
     

    }
}
