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
    public class TourAgentService : BusinessService<TourAgent, TourAgentDTO>, ITourAgentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private IRepository<TourAgent> _repository;
        public TourAgentService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
            _unitOfWork = unitOfWork;
            _repository = _unitOfWork.GetRepository<TourAgent>();
        }
        public static double Calculate(double sLatitude, double sLongitude, double eLatitude,
                               double eLongitude)
        {
            var radiansOverDegrees = (Math.PI / 180.0);

            var sLatitudeRadians = sLatitude * radiansOverDegrees;
            var sLongitudeRadians = sLongitude * radiansOverDegrees;
            var eLatitudeRadians = eLatitude * radiansOverDegrees;
            var eLongitudeRadians = eLongitude * radiansOverDegrees;

            var dLongitude = eLongitudeRadians - sLongitudeRadians;
            var dLatitude = eLatitudeRadians - sLatitudeRadians;

            var result1 = Math.Pow(Math.Sin(dLatitude / 2.0), 2.0) +
                          Math.Cos(sLatitudeRadians) * Math.Cos(eLatitudeRadians) *
                          Math.Pow(Math.Sin(dLongitude / 2.0), 2.0);

            // Using 3956 as the number of miles around the earth
            var result2 = 3956.0 * 2.0 *
                          Math.Atan2(Math.Sqrt(result1), Math.Sqrt(1.0 - result1));

            return result2;
        }
        public List<AgentCheckPointDTO> GetAgentCheckPoints()
        {
            var agentCheckPoints = _unitOfWork.GetRepository<TourCheckPoint>().GetAll().OrderBy(x => x.Tour.TourDate)
                .Where(c => c.Tour.TourState == TourState.InProgress && c.Tour.TourDate.Date == DateTime.Now.Date)
                .Select(x => new AgentCheckPointDTO
                {
                    AgentId = x.Tour.AgentId,
                    Agent = new AgentDetailsDTO
                    {
                        AgentType = x.Tour.Agent.AgentType,
                        BirthDate = x.Tour.Agent.BirthDate,
                        CompanyId = x.Tour.Agent.CompanyId,
                        Email = x.Tour.Agent.Email,
                        FullNameAr = x.Tour.Agent.FullNameAr,
                        FullNameEn = x.Tour.Agent.FullNameEn,
                        Id = x.Tour.Agent.Id,
                        IsTempPassword = x.Tour.Agent.TempPassword,
                        JobTitle = x.Tour.Agent.JobTitle,
                        Mobile = x.Tour.Agent.Mobile,
                        SupervisorId = x.Tour.Agent.SupervisorId,
                        UserName = x.Tour.Agent.UserName
                    },
                    CheckPointNameAr = x.CheckPoint.CheckPointNameAr,
                    CheckPointNameEn = x.CheckPoint.CheckPointNameEn,
                    CheckPointState = x.TourCheckPointState,
                    distanceToNextPoint = 0,
                    Id = x.CheckPointId,
                    Lat = x.CheckPoint.Lat,
                    LocationText = x.CheckPoint.LocationText,
                    Long = x.CheckPoint.Long,

                }).ToList();
            for (int i = 0; i < agentCheckPoints.Count; i++)
            {
                if (agentCheckPoints[i].CheckPointState == TourCheckPointState.Completed)
                {
                    continue;
                }
                else
                {
                    if (i == agentCheckPoints.Count - 1)
                    {
                        continue;
                    }
                    agentCheckPoints[i].distanceToNextPoint = Calculate(agentCheckPoints[i].Lat, agentCheckPoints[i].Long, agentCheckPoints[i + 1].Lat, agentCheckPoints[i + 1].Long);
                }
            }
            return agentCheckPoints;
        }
        //private void CustomeMethod()
        //{
        //    var TourAgent=_repository.
        //}
    }
}