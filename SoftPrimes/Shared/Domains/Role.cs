using System.Collections.Generic;

namespace SoftPrimes.Shared.Domains
{
    public class Role
    {
        public int Id { get; set; }
        public string RoleNameAr { get; set; }
        public string RoleNameEn{ get; set; }
        public virtual List<RolePermission> Permissions { get; set; }
    }
}