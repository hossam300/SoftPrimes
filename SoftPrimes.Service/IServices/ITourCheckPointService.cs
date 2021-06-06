using SoftPrimes.Shared.Domains;
using SoftPrimes.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftPrimes.Service.IServices
{
    public interface ITourCheckPointService : IBusinessService<TourCheckPoint, TourCheckPointDTO>
    {
        bool ScanLocationQrCode(LocationQrCodeDTO locationQrCode);
        CommentDTO AddCheckPointTourComment(CheckPointTourCommentDetailDTO checkPointTourComment,string Url);
        bool ChangeTourCheckPointState(int tourCheckPointId,int State);
    }
}
