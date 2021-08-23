using AutoMapper;
using SoftPrimes.Shared.Domains;
using SoftPrimes.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftPrimes.BLL.Mappings
{
    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<RolePermission, RolePermissionDTO>().ReverseMap();
        }
    }
}
