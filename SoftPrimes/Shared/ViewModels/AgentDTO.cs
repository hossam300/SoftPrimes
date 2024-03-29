﻿using SoftPrimes.Shared.Domains;
using System;
using System.Collections.Generic;

namespace SoftPrimes.Shared.ViewModels
{
    public class AgentDTO
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string JobTitle { get; set; }
        public string Mobile { get; set; }
        public DateTime BirthDate { get; set; }
        public string FullNameAr { get; set; }
        public string FullNameEn { get; set; }
        public byte[] Image { get; set; }
        public AgentType AgentType { get; set; }
        public bool Active { get; set; }
        public bool TempPassword { get; set; } = false;
        public string SupervisorId { get; set; }
        public AgentDTO Supervisor { get; set; }
        public List<AgentDTO> Agents { get; set; }
        public int CompanyId { get; set; }
        public CompanyDTO Company { get; set; }
        public virtual List<AgentRoleDTO> AgentRoles { get; set; }
    }
}