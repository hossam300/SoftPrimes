using AutoMapper;
using IHelperServices.Models;
using IHelperServices;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using SoftPrimes.BLL.AuthenticationServices;
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
    public class DashbordService : BusinessService<TourAgent, TourAgentDTO>, IDashbordService
    {
        private readonly IUnitOfWork _uow;
        public DashbordService(IUnitOfWork unitOfWork, IMapper mapper, ISecurityService securityService, IHttpContextAccessor contextAccessor, IHelperServices.ISessionServices sessionServices, IOptions<AppSettings> appSettings, IDataProtectService dataProtectService, IOptionsSnapshot<BearerTokensOptions> configuration, IMailServices mailServices)
           : base(unitOfWork, mapper, securityService, sessionServices, appSettings)
        {
            _uow = unitOfWork;
        }

        public List<PiChartDTO> CheckPointCount(DateTime? start, DateTime? end)
        {
            return _uow.GetRepository<TourCheckPoint>().GetAll().GroupBy(x => x.CheckPoint.CheckPointNameEn).Select(x => new PiChartDTO
            {
                Text = x.Key,
                Value = x.Count()
            }).ToList();

        }

        public TourVsMontringDate TourMontringVsDate(DateTime? start, DateTime? end)
        {
            var MontringDate = _uow.GetRepository<TourAgent>().GetAll()
                 .GroupBy(x => new { Month = x.TourDate.Month, Year = x.TourDate.Year }).Select(x => new LineChartWithdate
                 {
                     Date = x.Key.Month + "-" + x.Key.Year,
                     Value = x.Where(c => c.TourType == TourType.Monitoring).Count()
                 }).ToList();
            var TourVsDate = _uow.GetRepository<TourAgent>().GetAll()
                 .GroupBy(x => new { Month = x.TourDate.Month, Year = x.TourDate.Year }).Select(x => new LineChartWithdate
                 {
                     Date = x.Key.Month + "-" + x.Key.Year,
                     Value = x.Where(c => c.TourType == TourType.TourPoints).Count()
                 }).ToList();
            return new TourVsMontringDate()
            {
                MontringVsDate = MontringDate,
                TourVsDate = TourVsDate
            };

        }

        public List<PiChartDTO> TourStatus(DateTime? start, DateTime? end)
        {
            return _uow.GetRepository<TourAgent>().GetAll().GroupBy(x => x.TourState)
                .Select(x => new PiChartDTO
                {
                    Text = x.Key.ToString(),
                    Value = x.Count()
                }).ToList();
        }
    }
}
