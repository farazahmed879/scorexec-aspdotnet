﻿using ScoringAppReact.Players.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScoringAppReact.LiveScore.Dto
{
    public class LiveScoreDto
    {
        public long MatchId { get; set; }
        public int CurrentInning { get; set; }
        public long PlayingTeamId { get; set; }
        public long BowlingTeamId { get; set; }
        public long? StrikerId { get; set; }
        public long? NonStrikerId { get; set; }
        public Dictionary<long, BatsmanDto> Batsmans { get; set; }
        public BowlerDto Bowler { get; set; }
        public ExtrasDto Extras { get; set; }
        public LiveTeamDto Team1 { get; set; }
        public LiveTeamDto Team2 { get; set; }
        public WicketDto HowOut { get; set; }
        public PartnershipDto Partnership { get; set; }
        public List<PlayerListDto> Players { get; set; }
        public int Overs { get; internal set; }
    }
}
