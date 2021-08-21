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
    public class CheckPointService : BusinessService<CheckPoint, CheckPointDTO>, ICheckPointService
    {
        private readonly IUnitOfWork _unitOfWork;
        private IRepository<CheckPoint> _repository;
        public CheckPointService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
            _unitOfWork = unitOfWork;
            _repository = _unitOfWork.GetRepository<CheckPoint>();
        }

        public List<CheckPointDTO> GetCheckPointLookups(string searchText, int take)
        {
            if (searchText == "" || string.IsNullOrEmpty(searchText) || searchText == null)
            {
                return _repository.GetAll().Select(x => new CheckPointDTO
                {
                    Id = x.Id,
                    CheckPointNameAr = x.CheckPointNameAr,
                    CheckPointNameEn = x.CheckPointNameEn,
                    Lat=x.Lat,
                    LocationText=x.LocationText,
                    Long=x.Long,
                    QRCode=x.QRCode
                }).Take(take).ToList();
            }
            else
            {
                return _repository.GetAll().Where(x => x.CheckPointNameAr.Contains(searchText) || x.CheckPointNameEn.Contains(searchText))
                    .Select(x => new CheckPointDTO
                    {
                        Id = x.Id,
                        CheckPointNameAr = x.CheckPointNameAr,
                        CheckPointNameEn = x.CheckPointNameEn,
                        Lat = x.Lat,
                        LocationText = x.LocationText,
                        Long = x.Long,
                        QRCode = x.QRCode
                    }).Take(take).ToList();
            }
        }
        //private void CustomeMethod()
        //{
        //    var CheckPoint=_repository.
        //}
    }
}