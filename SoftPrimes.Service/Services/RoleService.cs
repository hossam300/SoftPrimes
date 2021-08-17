using AutoMapper;
using SoftPrimes.BLL.BaseObjects.RepositoriesInterfaces;
using SoftPrimes.Service.IServices;
using SoftPrimes.Shared.Domains;
using SoftPrimes.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftPrimes.Service.Services
{
    public class RoleService : BusinessService<Role, RoleDTO>, IRoleService
    {
        private readonly IUnitOfWork _unitOfWork;
        private IRepository<Role> _repository;
        public RoleService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
            _unitOfWork = unitOfWork;
            _repository = _unitOfWork.GetRepository<Role>();
        }
        public override IEnumerable<RoleDTO> Update(IEnumerable<RoleDTO> Entities)
        {
            foreach (var role in Entities)
            {
                var RolePermission = _unitOfWork.GetRepository<RolePermission>().GetAll().Where(x => x.RoleId == role.Id).ToList();
                if (RolePermission.Count() != 0)
                {
                    _unitOfWork.GetRepository<RolePermission>().Delete(RolePermission);
                    _unitOfWork.SaveChanges();
                }
            }
            return base.Update(Entities);
        }
    }
}