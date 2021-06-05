using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SoftPrimes.Service.IServices;
using SoftPrimes.Shared.Domains;
using SoftPrimes.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Geolocation;
using SoftPrimes.BLL.BaseObjects.RepositoriesInterfaces;

namespace SoftPrimes.Service.Services
{
    public class TourService : BusinessService<Tour, TourDTO>, ITourService
    {
        private readonly IUnitOfWork _unitOfWork;
        private IRepository<Tour> _repository;
        public TourService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
            _unitOfWork = unitOfWork;
            _repository = _unitOfWork.GetRepository<Tour>();
        }

        public List<HomeTourDTO> GetTodayTours(float lat, float longs, string AgentId)
        {
            var agent = _unitOfWork.GetRepository<Agent>().GetAll().Select(x => new AgentDTO
            {
                Id = x.Id,
                Active = x.Active,
                FullNameAr = x.FullNameAr,
                FullNameEn = x.FullNameEn,
                AgentType = x.AgentType,
                CompanyId = x.CompanyId,
                UserName = x.UserName,
                Email = x.Email,
                Image = x.Image

            }).FirstOrDefault(c => c.Id == AgentId);
            return _unitOfWork.GetRepository<TourAgent>().GetAll().Include(x => x.Tour)
                    .Include(x => x.CheckPoints).ThenInclude(x => x.CheckPoint)
                    .Where(x => x.TourDate.Date == DateTime.Now.Date && x.AgentId == AgentId).Select(x => new HomeTourDTO
                    {
                        Agent = agent,
                        AgentId = x.AgentId,
                        EstimateDistance = (float)GetEstimateDistance(lat, longs, (float)x.CheckPoints.First().CheckPoint.Lat, (float)x.CheckPoints.First().CheckPoint.Long),
                        LocationAr = x.CheckPoints.FirstOrDefault().CheckPoint.CheckPointNameAr,
                        LocationEn = x.CheckPoints.FirstOrDefault().CheckPoint.CheckPointNameAr,
                        TimeDuration = x.CreatedOn.Value.ToString("t") + ":" + x.EstimatedEndDate.Value.ToString("t"),
                        TourDate = x.TourDate.DateTime,
                        TourId = x.TourId,
                        TourNameAr = x.Tour.TourNameAr,
                        TourNameEn = x.Tour.TourNameEn,
                        TourType = x.TourType.ToString(),
                        CountOfLocations = x.CheckPoints.Count()
                    }).ToList();
        }

        private static double GetEstimateDistance(float Cuttentlat, float Cuttentlongs, float DistanceLat, float DistanceLong)
        {
            return GeoCalculator.GetDistance(new Coordinate(Cuttentlat, Cuttentlongs), new Coordinate(DistanceLat, DistanceLong));
        }

        public List<TourCheckPointDTO> GetTourPoints(int tourId)
        {
            return _unitOfWork.GetRepository<TourCheckPoint>().GetAll()
                .Include(x => x.Tour).ThenInclude(x => x.CheckPoints).ThenInclude(x => x.CheckPoint)
                .Include(x => x.CheckPoint)
                .Include(x => x.CheckPointTourComments).ThenInclude(x => x.Comment)
                .ThenInclude(x => x.Attachment)
                .Where(x => x.TourId == tourId).Select(x => new TourCheckPointDTO
                {
                    CheckPoint = new CheckPointDTO
                    {
                        CheckPointNameAr = x.CheckPoint.CheckPointNameAr,
                        CheckPointNameEn = x.CheckPoint.CheckPointNameEn,
                        Lat = x.CheckPoint.Lat,
                        Id = x.CheckPointId,
                        Long = x.CheckPoint.Long,
                        QRCode = x.CheckPoint.QRCode
                    },
                    CheckPointId = x.CheckPointId,
                    CheckPointTourComments = x.CheckPointTourComments.Select(x => new CheckPointTourCommentDTO
                    {
                        Comment = new CommentDTO
                        {
                            Attachment = new AttachmentDTO
                            {
                                AttachmentName = x.Comment.Attachment.AttachmentName,
                                AttachmentType = x.Comment.Attachment.AttachmentType,
                                AttachmentUrl = x.Comment.Attachment.AttachmentUrl,
                                Id = x.Comment.Attachment.Id
                            },
                            AttachmentId = x.Comment.AttachmentId,
                            Id = x.CommentId,
                            ReplayToComment = x.Comment.ReplayToComment,
                            Text = x.Comment.Text
                        }
                    }).ToList(),
                    Id = x.Id,
                    TourCheckPointState = x.TourCheckPointState,
                    TourId = x.TourId,
                    Tour = new TourAgentDTO
                    {
                        CheckPoints = x.Tour.CheckPoints.Select(y => new TourCheckPointDTO
                        {
                            CheckPoint = new CheckPointDTO
                            {
                                CheckPointNameAr = y.CheckPoint.CheckPointNameAr,
                                CheckPointNameEn = y.CheckPoint.CheckPointNameEn,
                                Id = y.CheckPointId,
                                Lat = y.CheckPoint.Lat,
                                Long = y.CheckPoint.Long,
                                QRCode = y.CheckPoint.QRCode
                            },
                            CheckPointId = y.CheckPointId,
                            Id = y.Id,
                            TourCheckPointState = y.TourCheckPointState,
                            TourId = x.TourId
                        }).ToList(),
                    }
                }).ToList();
        }
        //private void CustomeMethod()
        //{
        //    var Tour=_repository.
        //}
    }
}