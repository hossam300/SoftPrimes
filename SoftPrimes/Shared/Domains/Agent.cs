﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftPrimes.Shared.Domains
{
    public class Agent : IdentityUser
    {
        public string FullNameAr { get; set; }
        public string FullNameEn { get; set; }
        public byte[] Image { get; set; }
        public AgentType AgentType { get; set; }
        public bool Active { get; set; }
        public string SupervisorId { get; set; }
        public Agent Supervisor { get; set; }
        public List<Agent> Agents { get; set; }
        public int CompanyId { get; set; }
        public Company Company { get; set; }
    }
    public enum AgentType
    {
        NormalAgent = 1,
        Supervisor = 2,
        Admin = 3
    }
}
