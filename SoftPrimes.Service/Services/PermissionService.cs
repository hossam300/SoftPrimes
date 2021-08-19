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
    public class PermissionService : BusinessService<Permission, PermissionDTO>, IPermissionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private IRepository<Permission> _repository;
        public PermissionService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
            _unitOfWork = unitOfWork;
            _repository = _unitOfWork.GetRepository<Permission>();
        }

        public List<PermissionDTO> GetPermissionLookups(string searchText)
        {
            if (searchText == "" || string.IsNullOrEmpty(searchText) || searchText == null)
            {
                return _repository.GetAll().Select(x => new PermissionDTO
                {
                    Id = x.Id,
                    PermissionKey = x.PermissionKey,
                    PermissionNameAr = x.PermissionNameAr,
                    PermissionNameEn = x.PermissionNameEn
                }).ToList();
            }
            else
            {
                return _repository.GetAll().Where(x => x.PermissionKey.Contains(searchText) || x.PermissionNameAr.Contains(searchText) || x.PermissionNameEn.Contains(searchText))
                    .Select(x => new PermissionDTO
                    {
                        Id = x.Id,
                        PermissionKey = x.PermissionKey,
                        PermissionNameAr = x.PermissionNameAr,
                        PermissionNameEn = x.PermissionNameEn
                    }).ToList();
            }
        }
    }
}