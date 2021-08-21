using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftPrimes.Shared.ViewModels
{
    public class RoleDetailsDTO
    {
        public int Id { get; set; }
        public string RoleNameAr { get; set; }
        public string RoleNameEn { get; set; }
        public virtual List<RolePermissionDTO> Permissions { get; set; }
    }
}
