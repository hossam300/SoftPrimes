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
    public class TourCheckPointService : BusinessService<TourCheckPoint, TourCheckPointDTO>, ITourCheckPointService
    {
        private readonly IUnitOfWork _unitOfWork;
        private IRepository<TourCheckPoint> _repository;
        IMapper _mapper;
        public TourCheckPointService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
            _unitOfWork = unitOfWork;
            _repository = _unitOfWork.GetRepository<TourCheckPoint>();
            _mapper = mapper;
        }

        public CommentDTO AddCheckPointTourComment(CheckPointTourCommentDetailDTO checkPointTourComment, string Url)
        {
            Comment comment = new Comment
            {
                Attachment = Url == "" || checkPointTourComment.AttachmentType == 0 ? null : new Attachment
                {
                    AttachmentName = checkPointTourComment.Text,
                    AttachmentType = (AttachmentType)checkPointTourComment.AttachmentType,
                    AttachmentUrl = Url,
                },
                Text = checkPointTourComment.Text
            };
            var Newcomment = _unitOfWork.GetRepository<Comment>().Insert(comment);
            CheckPointTourComment tourComment = new CheckPointTourComment
            {
                CommentId = comment.Id,
                TourCheckPointId = checkPointTourComment.CheckPointId
            };
            _unitOfWork.GetRepository<CheckPointTourComment>().Insert(tourComment);
            return _mapper.Map(Newcomment, typeof(Comment), typeof(CommentDTO)) as CommentDTO;
        }

        public bool ChangeTourCheckPointState(int tourCheckPointId, int State)
        {
            var tourChexkPoint = _repository.Find(tourCheckPointId);
            if (tourChexkPoint == null)
            {
                return false;
            }
            else
            {
                try
                {
                    tourChexkPoint.TourCheckPointState = (TourCheckPointState)State;
                    _repository.Update(tourChexkPoint);
                    return true;
                }
                catch (Exception ex)
                {

                    return false;
                }
            }
        }

        public bool ScanLocationQrCode(LocationQrCodeDTO locationQrCode)
        {
            var TourCheckPoint = _repository.GetAll().Where(x => x.Id == locationQrCode.CheckPointId || x.CheckPoint.QRCode == locationQrCode.QRCode).FirstOrDefault();
            if (TourCheckPoint == null)
            {
                return false;
            }
            else
            {

                if (TourCheckPoint.Tour.TourType == TourType.Monitoring && TourCheckPoint.TourCheckPointState == TourCheckPointState.New)
                {
                    TourCheckPoint.TourCheckPointState = TourCheckPointState.InProgress;
                }
                else
                {
                    TourCheckPoint.TourCheckPointState = TourCheckPointState.Completed;
                }
                _repository.Update(TourCheckPoint);
                return true;
            }
        }
        //private void CustomeMethod()
        //{
        //    var TourCheckPoint=_repository.
        //}
    }
}