﻿using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;

namespace ScoringAppReact.Models
{
    public class Ground : FullAuditedEntity<long>, IMayHaveTenant
    {
        [MaxLength(25)]
        public string Name { get; set; }
        [MaxLength(25)]
        public string Location { get; set; }
        public int? TenantId { get; set; }
    }
}