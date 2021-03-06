using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.Authorization.Roles;
using Abp.AutoMapper;
using ScoringAppReact.Authorization.Roles;
using ScoringAppReact.Models;
using ScoringAppReact.TeamScores.Dto;

namespace ScoringAppReact.Teams.Dto
{
    public class MatchTeamDto : EntityDto<long>
    {
        public string Name { get; set; }
        public string Place { get; set; }
        public string Zone { get; set; }
        public string Contact { get; set; }
        public bool IsRegistered { get; set; }
        public string City { get; set; }
        public string FileName { get; set; }
        public int Type { get; set; }
        public TeamScoreDto TeamScore { get; set; }
    }
}