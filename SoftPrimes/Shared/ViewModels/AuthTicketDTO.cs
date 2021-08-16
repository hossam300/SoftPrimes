using SoftPrimes.Shared.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftPrimes.Shared.ViewModels
{
    public class AuthTicketDTO
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string FullNameAr { get; set; }
        public string FullNameEn { get; set; }
        public string UserId { get; set; }
        public string JobTitle { get; set; }
        public string Mobile { get; set; }
        public DateTime BirthDate { get; set; }
        public string SupervisorId { get; set; }
        public int CompanyId { get; set; }
        public virtual List<AgentRoleDTO> AgentRoles { get; set; }
    }
}
