﻿using AutoMapper;
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
using IHelperServices;

namespace SoftPrimes.Service.Services
{
    public class TourService : BusinessService<Tour, TourDTO>, ITourService
    {
        private readonly IUnitOfWork _unitOfWork;
        private IRepository<Tour> _repository;
        ISessionServices _sessionServices;
        public TourService(IUnitOfWork unitOfWork, IMapper mapper, ISessionServices sessionServices) : base(unitOfWork, mapper)
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
                        EstimateDistance = (float)x.EstimatedDistance,
                        LocationAr = x.CheckPoints.FirstOrDefault().CheckPoint.CheckPointNameAr,
                        LocationEn = x.CheckPoints.FirstOrDefault().CheckPoint.CheckPointNameAr,
                        StartDate = x.CheckPoints.FirstOrDefault().StartDate,
                        EndDate = x.CheckPoints.FirstOrDefault().EndDate,
                        //  TimeDuration = x.CheckPoints.FirstOrDefault().StartDate.ToString("t") + ":" + x.CheckPoints.OrderBy(x=>x.EndDate).LastOrDefault().EndDate.ToString("t"),
                        TourDate = x.TourDate.DateTime,
                        TourId = x.Id,
                        TourNameAr = x.Tour.TourNameAr,
                        TourNameEn = x.Tour.TourNameEn,
                        TourType = x.TourType.ToString(),
                        TourTypeId = (int)x.TourType,
                        CountOfLocations = x.CheckPoints.Count(),
                        TourState = x.TourState
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
                .Where(x => x.Id == tourId).Select(x => new TourCheckpointDetailsDTO
                {
                    Id = x.Id,
                    TourNameAr = x.Tour.TourNameAr,
                    TourNameEn = x.Tour.TourNameEn,
                    TourDate = x.TourDate,
                    TourState = x.TourState,
                    AdminCommnets = x.Comments.Select(y =>
                    new CommentDetailsDTO
                    {

                        CommentByNameAr = _unitOfWork.GetRepository<Agent>(false).Find(x.CreatedBy).FullNameAr,
                        CommentByNameEn = _unitOfWork.GetRepository<Agent>(false).Find(x.CreatedBy).FullNameEn,
                        Id = y.Id,
                        ProfileImage = _unitOfWork.GetRepository<Agent>(false).Find(x.CreatedBy).Image,
                        Text = y.Comment.Text
                    }).ToList(),
                    CheckPoints = x.CheckPoints.Select(y => new CheckPointDetailsDTO
                    {
                        CheckPointNameAr = y.CheckPoint.CheckPointNameAr,
                        CheckPointNameEn = y.CheckPoint.CheckPointNameEn,
                        CheckPointState = y.TourCheckPointState,
                        StartDate = y.StartDate,
                        EndDate = x.EstimatedEndDate,
                        Lat = y.CheckPoint.Lat,
                        Long = y.CheckPoint.Long,
                        EstimatedDistance = x.EstimatedDistance,
                        Id = y.Id,
                        LocationName = y.CheckPoint.LocationText,
                        QRCode = y.CheckPoint.QRCode.ToString(),
                        Comments = y.CheckPointTourComments.Count > 0
                                    ? y.CheckPointTourComments.Select(z => new AttachmentCommentDTO
                                    {
                                        AttachmentName = z.Comment != null ? (z.Comment.Attachment != null) ? z.Comment.Attachment.AttachmentName : null : null,
                                        AttachmentType = z.Comment != null ? (z.Comment.Attachment != null) ? z.Comment.Attachment.AttachmentType : 0 : 0,
                                        AttachmentUrl = z.Comment != null ? (z.Comment.Attachment != null) ? z.Comment.Attachment.AttachmentUrl : null : null,
                                        //Attachment = z.Comment != null ? (z.Comment.Attachment != null) ? new AttachmentDTO
                                        //{
                                        //    AttachmentName = z.Comment.Attachment.AttachmentName,
                                        //    AttachmentType = z.Comment.Attachment.AttachmentType,
                                        //    AttachmentUrl = z.Comment.Attachment.AttachmentUrl,
                                        //    Id = (int)z.Comment.AttachmentId,
                                        //} : null : null,
                                        AttachmentId = z.Comment != null ? z.Comment.AttachmentId : null,
                                        Id = z.CommentId,
                                        Text = z.Comment != null ? z.Comment.Text : null,
                                        ReplayToComment = z.Comment != null ? z.Comment.ReplayToComment : null
                                    }).ToList() : new List<AttachmentCommentDTO>()

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

        public bool ChangeTourState(int tourId, int state, double lat, double longs)
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
                    tour.CheckoutLong = longs;
                    tour.CheckoutLat = lat;
                    tour.CheckoutDate = DateTime.Now;
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
                        StartDate = x.CheckPoints.FirstOrDefault().StartDate,
                        EndDate = x.CheckPoints.FirstOrDefault().EndDate,
                        TourDate = x.TourDate.DateTime,
                        TourId = x.TourId,
                        TourNameAr = x.Tour.TourNameAr,
                        TourNameEn = x.Tour.TourNameEn,
                        TourType = x.TourType.ToString(),
                        TourTypeId = (int)x.TourType,
                        CountOfLocations = x.CheckPoints.Count(),
                        TourState = x.TourState
                    }).ToList();
        }

        public List<TourTemplateDTO> GetTemplates(string searchText, int take)
        {
            if (searchText == "" || string.IsNullOrEmpty(searchText) || searchText == null)
            {
                return _unitOfWork.GetRepository<TourAgent>().GetAll().Where(x => x.IsTemplate).Select(x => new TourTemplateDTO
                {
                    Id = x.TourId,
                    TourNameAr = x.Tour.TourNameAr,
                    TourNameEn = x.Tour.TourNameEn,
                    Active = x.Tour.Active,
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
                        TourId = y.Id
                    }).ToList()

                }).Take(take).ToList();
            }
            else
            {
                return _unitOfWork.GetRepository<TourAgent>().GetAll()
                    .Where(x => x.IsTemplate && (x.Tour.TourNameAr.Contains(searchText) || x.Tour.TourNameEn.Contains(searchText))).Select(x => new TourTemplateDTO
                    {
                        Id = x.TourId,
                        TourNameAr = x.Tour.TourNameAr,
                        TourNameEn = x.Tour.TourNameEn,
                        Active = x.Tour.Active,
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

                    }).Take(take).ToList();
            }
        }

        public TourCreateDTO InsertTour(TourCreateDTO tour)
        {
            var template = new Tour();
            if (tour.TourId != null && tour.TourId != 0)
            {
                template = _repository.GetAll().FirstOrDefault(c => c.Id == tour.TourId);
                if (template.TourNameEn != tour.TourName)
                {
                    template = _repository.Insert(new Tour { TourNameEn = tour.TourName, Active = true });
                }
            }
            else
            {
                template = _repository.Insert(new Tour { TourNameEn = tour.TourName, Active = true });
            }
            List<TourComment> tourComments = new List<TourComment>();
            if (tour.AdminComment != "" || tour.AdminComment != null)
            {
                tourComments.Add(new TourComment
                {
                    Comment = new Comment
                    {
                        Text = tour.AdminComment
                    },
                    TourId = template.Id,
                });
            }
            TourAgent tourAgent = _unitOfWork.GetRepository<TourAgent>().Insert(new TourAgent
            {
                AgentId = tour.AgentId,
                IsTemplate = tour.IsTemplate,
                TourId = template.Id,
                TourDate = new DateTimeOffset(tour.TourDate.Year, tour.TourDate.Month, tour.TourDate.AddDays(1).Day, 0, 0, 0, TimeSpan.Zero),
                TourState = TourState.New,
                TourType = tour.TourType,
                EstimatedEndDate = tour.PointLocations.OrderBy(x => x.EndDate).LastOrDefault().EndDate,
                EstimatedDistance = tour.EstimatedDistance,
                CheckPoints = tour.PointLocations.Select(x => new TourCheckPoint
                {
                    CheckPointId = x.CheckPointId,
                    EndDate = x.EndDate,
                    StartDate = x.StartDate
                }).ToList(),
                Comments = tourComments
            });
            NotificationDTO notification = new NotificationDTO
            {

                CreatedBy = _sessionServices.UserId,
                CreatedOn = DateTime.Now,
                IsReaded = false,
                NotificationType = NotificationType.NewTour,
                Text = "Tour " + tourAgent.Tour.TourNameEn + " has been Created",
                ToAgentId = tourAgent.AgentId
            };
            _unitOfWork.GetRepository<NotificationDTO>().Insert(notification);
            tour.Id = tourAgent.Id;
            return tour;
        }

        public bool ActiveDisActiveTemplate(int tourId, bool state)
        {
            try
            {
                var tour = _repository.GetById(tourId);
                tour.Active = state;
                _repository.Update(tour);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        //private void CustomeMethod()
        //{
        //    var Tour=_repository.
        //}
    }
}