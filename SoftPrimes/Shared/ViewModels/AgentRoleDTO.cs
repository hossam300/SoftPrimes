using SoftPrimes.Shared.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftPrimes.Shared.ViewModels
{
   public class AgentRoleDTO
    {
        public int Id { get; set; }
        public string AgentId { get; set; }
        public virtual AgentDTO Agent { get; set; }
        public int RoleId { get; set; }
        public virtual RoleDTO Role { get; set; }
    }
}
