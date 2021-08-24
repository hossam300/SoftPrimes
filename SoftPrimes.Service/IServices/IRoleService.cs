using SoftPrimes.Shared.Domains;
using SoftPrimes.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftPrimes.Service.IServices
{
    public interface IRoleService : IBusinessService<Role, RoleDTO>
    {
        IEnumerable<RoleDetailsDTO> InsertRole(IEnumerable<RoleDetailsDTO> entities);
        List<RoleDTO> GetRoleLookups(string searchText,int take);
    IEnumerable<RoleDetailsDTO> UpdateRole(IEnumerable<RoleDetailsDTO> entities);
  }
}
