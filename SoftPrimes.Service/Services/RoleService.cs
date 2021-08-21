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
            var newrole = base.Update(Entities);
            return newrole;
        }
        public override IEnumerable<RoleDTO> Insert(IEnumerable<RoleDTO> entities)
        {
            foreach (var item in entities)
            {
                var role = new Role
                {
                    RoleNameAr = item.RoleNameAr,
                    RoleNameEn = item.RoleNameEn,
                    Permissions = item.Permissions.Select(x => new RolePermission
                    {
                        PermissionId = x
                    }).ToList()
                };
                _unitOfWork.GetRepository<Role>().Insert(role);
            }
            return entities;
        }

        public List<RoleDTO> GetRoleLookups(string searchText, int take)
        {
            if (searchText == "" || string.IsNullOrEmpty(searchText) || searchText == null)
            {
                return _repository.GetAll().Select(x => new RoleDTO
                {
                    Id = x.Id,
                    RoleNameAr = x.RoleNameAr,
                    RoleNameEn = x.RoleNameEn
                }).Take(take).ToList();
            }
            else
            {
                return _repository.GetAll().Where(x => x.RoleNameAr.Contains(searchText) || x.RoleNameEn.Contains(searchText))
                    .Select(x => new RoleDTO
                    {
                        Id = x.Id,
                        RoleNameAr = x.RoleNameAr,
                        RoleNameEn = x.RoleNameEn
                    }).Take(take).ToList();
            }
        }
    }
}