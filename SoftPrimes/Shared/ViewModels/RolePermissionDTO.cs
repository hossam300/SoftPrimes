using SoftPrimes.Shared.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftPrimes.Shared.ViewModels
{
    public class RolePermissionDTO
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public virtual RoleDTO Role { get; set; }
        public int PermissionId { get; set; }
        public virtual PermissionDTO Permission { get; set; }
    }
}
