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
                        TourTypeId = (int)x.TourType,
                        CountOfLocations = x.CheckPoints.Count()
                    }).ToList();
        }

        private static double GetEstimateDistance(float Cuttentlat, float Cuttentlongs, float DistanceLat, float DistanceLong)
        {
            return GeoCalculator.GetDistance(new Coordinate(Cuttentlat, Cuttentlongs), new Coordinate(DistanceLat, DistanceLong));
        }

        public TourCheckpointDetailsDTO GetTourPoints(int tourId)
        {
            return _unitOfWork.GetRepository<TourAgent>().GetAll()
                .Include(x => x.Tour).Include(x => x.CheckPoints).ThenInclude(x => x.CheckPoint)
               .Include(x => x.CheckPoints).ThenInclude(x => x.CheckPointTourComments).ThenInclude(x => x.Comment)
                .ThenInclude(x => x.Attachment)
                .Where(x => x.TourId == tourId).Select(x => new TourCheckpointDetailsDTO
                {
                    Id = x.Id,
                    TourNameAr = x.Tour.TourNameAr,
                    TourNameEn = x.Tour.TourNameEn,
                    TourDate = x.TourDate,
                    TourState = x.TourState,
                    AdminCommnets = x.Comments.Select(y =>
                    new CommentDetailsDTO
                    {

                        CommentByNameAr = _unitOfWork.GetRepository<Agent>(false).Find(y.CreatedBy).FullNameAr,
                        CommentByNameEn = _unitOfWork.GetRepository<Agent>(false).Find(y.CreatedBy).FullNameEn,
                        Id = y.Id,
                        ProfileImage = _unitOfWork.GetRepository<Agent>(false).Find(y.CreatedBy).Image,
                        Text = y.Comment.Text
                    }).ToList(),
                    CheckPoints = x.CheckPoints.Select(y => new CheckPointDetailsDTO
                    {
                        CheckPointNameAr = y.CheckPoint.CheckPointNameAr,
                        CheckPointNameEn = y.CheckPoint.CheckPointNameEn,
                        CheckPointState = y.TourCheckPointState,
                        EndDate = x.EstimatedEndDate,
                        EstimatedDistance = x.EstimatedDistance,
                        Id = y.Id,
                        LocationName = y.CheckPoint.LocationText,
                        QRCode = y.CheckPoint.QRCode.ToString(),
                        Comments = y.CheckPointTourComments.Count > 0 ? y.CheckPointTourComments.Select(z => new CommentDTO
                        {
                            Attachment = z.Comment != null ? (z.Comment.Attachment != null) ? new AttachmentDTO
                            {
                                AttachmentName = z.Comment.Attachment.AttachmentName,
                                AttachmentType = z.Comment.Attachment.AttachmentType,
                                AttachmentUrl = z.Comment.Attachment.AttachmentUrl,
                                Id = (int)z.Comment.AttachmentId,
                            } : null : null,
                            AttachmentId = z.Comment != null ? z.Comment.AttachmentId : null,
                            Id = z.CommentId,
                            Text = z.Comment != null ? z.Comment.Text : null,
                            ReplayToComment = z.Comment != null ? z.Comment.ReplayToComment : null
                        }).ToList() : new List<CommentDTO>()

                    }).ToList()
                }).FirstOrDefault();
        }

        public List<TourCommentDTO> GetAdminComments(int tourId)
        {
            var comments = _unitOfWork.GetRepository<TourComment>().GetAll().Include(x => x.Comment)
                .Where(x => x.TourId == tourId);
            return comments.Select(x => new TourCommentDTO
            {
                Comment = new CommentDTO
                {
                    Attachment = x.Comment != null ? (x.Comment.Attachment != null) ? new AttachmentDTO
                    {
                        AttachmentName = x.Comment.Attachment.AttachmentName,
                        AttachmentType = x.Comment.Attachment.AttachmentType,
                        AttachmentUrl = x.Comment.Attachment.AttachmentUrl,
                        Id = (int)x.Comment.AttachmentId,
                    } : null : null,
                    ReplayToComment = x.Comment.ReplayToComment,
                    Id = x.CommentId,
                    Text = x.Comment.Text
                },
                CommentId = x.CommentId,
                CreatedBy = x.CreatedBy,
                CreatedOn = x.CreatedOn,
                Id = x.Id,
                TourId = x.TourId,
                CreatedByUser = _unitOfWork.GetRepository<Agent>(false).GetAll(false).Select(y => new AgentDTO
                {
                    Id = y.Id,
                    FullNameAr = y.FullNameAr,
                    FullNameEn = y.FullNameEn,
                    Image = y.Image,
                    UserName = y.UserName
                }).Where(c => c.Id == x.Comment.CreatedBy).FirstOrDefault(),
            }).ToList();
        }

        public bool ChangeTourState(int tourId, int state)
        {
            var tour = _unitOfWork.GetRepository<TourAgent>().GetAll().FirstOrDefault(c => c.Id == tourId);
            if (tour == null)
            {
                return false;
            }
            else
            {
                try
                {
                    tour.TourState = (TourState)state;
                    _unitOfWork.GetRepository<TourAgent>().Update(tour);
                    return true;
                }
                catch (Exception ex)
                {

                    return false;
                }
            }
        }

        public List<HomeTourDTO> GetTourHistory(float lat, float longs, string AgentId)
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
                    .Where(x => x.TourDate.Date < DateTime.Now.Date && x.AgentId == AgentId).Select(x => new HomeTourDTO
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
                        TourTypeId = (int)x.TourType,
                        CountOfLocations = x.CheckPoints.Count()
                    }).ToList();
        }

        public List<TourTemplateDTO> GetTemplates()
        {
            return _unitOfWork.GetRepository<TourAgent>().GetAll().Where(x => x.IsTemplate).Select(x => new TourTemplateDTO
            {
                Id = x.TourId,
                TourNameAr = x.Tour.TourNameAr,
                TourNameEn = x.Tour.TourNameEn,
                CheckPoints = x.CheckPoints.Select(y => new TourCheckPointDTO
                {
                    CheckPoint = new CheckPointDTO
                    {
                        CheckPointNameAr = y.CheckPoint.CheckPointNameAr,
                        CheckPointNameEn = y.CheckPoint.CheckPointNameEn,
                        Id = y.CheckPointId,
                        Lat = y.CheckPoint.Lat,
                        LocationText = y.CheckPoint.LocationText,
                        Long = y.CheckPoint.Long,
                        QRCode = y.CheckPoint.QRCode
                    },
                    CheckPointId = y.CheckPointId,
                    EndDate = y.EndDate,
                    StartDate = y.StartDate,
                    Id = y.Id,
                    TourId = y.TourId
                }).ToList()

            }).ToList();
        }

        //private void CustomeMethod()
        //{
        //    var Tour=_repository.
        //}
    }
}