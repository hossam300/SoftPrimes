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
using Geolocation;
using System.Drawing;

namespace SoftPrimes.Service.Services
{
    public class DashbordService : BusinessService<TourAgent, TourAgentDTO>, IDashbordService
    {
        private readonly IUnitOfWork _uow;
        private readonly ISessionServices _sessionServices;
        public DashbordService(IUnitOfWork unitOfWork, IMapper mapper, ISecurityService securityService, IHttpContextAccessor contextAccessor, IHelperServices.ISessionServices sessionServices, IOptions<AppSettings> appSettings, IDataProtectService dataProtectService, IOptionsSnapshot<BearerTokensOptions> configuration, IMailServices mailServices)
           : base(unitOfWork, mapper, securityService, sessionServices, appSettings)
        {
            _uow = unitOfWork;
            _sessionServices = sessionServices;
        }

        public List<PiChartDTO> AgentDistance(DateTime? start, DateTime? end)
        {
            return _uow.GetRepository<TourAgent>().GetAll().GroupBy(x => x.AgentId).Select(x => new PiChartDTO
            {
                Text = _sessionServices.Culture == "ar" ? _uow.GetRepository<Agent>(false).Find(x.Key).FullNameAr : _uow.GetRepository<Agent>(false).Find(x.Key).FullNameEn,
                Value = x.Sum(z => z.EstimatedDistance)
            }).ToList();
        }

        public List<PiChartDTO> CheckPointCount(DateTime? start, DateTime? end)
        {
            return _uow.GetRepository<TourCheckPoint>().GetAll().GroupBy(x => x.CheckPoint.CheckPointNameEn).Select(x => new PiChartDTO
            {
                Text = x.Key,
                Value = x.Count()
            }).ToList();

        }

        public List<LineChartWithdate> OverDue(DateTime? start, DateTime? end)
        {
            return _uow.GetRepository<TourAgent>().GetAll()
                .Where(x => x.TourState == TourState.Complete && x.CheckoutDate > x.TourDate)
                .GroupBy(x => new { Year = x.TourDate.Year, Month = x.TourDate.Month, Day = x.TourDate.Day })
                .Select(z => new LineChartWithdate
                {
                    Date = z.Key.Year + "-" + z.Key.Month + "-" + z.Key.Day,
                    Value = z.Count()
                }).ToList();
        }

        public TourVsMontringDate TourMontringVsDate(DateTime? start, DateTime? end)
        {
            var MontringDate = _uow.GetRepository<TourAgent>().GetAll()
                 .GroupBy(x => new { Month = x.TourDate.Month, Year = x.TourDate.Year, day = x.TourDate.Day }).Select(x => new LineChartWithdate
                 {
                   Date = x.Key.Year + "-" + x.Key.Month + "-" + x.Key.day,
                   Value = x.Where(c => c.TourType == TourType.Monitoring).Count()
                 }).ToList();
            var TourVsDate = _uow.GetRepository<TourAgent>().GetAll()
                 .GroupBy(x => new { Month = x.TourDate.Month, Year = x.TourDate.Year, day = x.TourDate.Day }).Select(x => new LineChartWithdate
                 {
                     Date = x.Key.Year + "-" + x.Key.Month + "-" + x.Key.day,
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
