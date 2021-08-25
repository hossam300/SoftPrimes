using SoftPrimes.Shared.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftPrimes.Shared.ViewModels
{
  public class AgentDetailsDTO
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
    public string SupervisorId { get; set; }
    public int RoleId { get; set; }
    public int CompanyId { get; set; }
    public AgentType AgentType { get; set; } = AgentType.NormalAgent;
    public bool IsTempPassword { get; set; } = false;

  }
}
