using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SoftPrimes.Service.IServices;
using SoftPrimes.Shared.Domains;
using SoftPrimes.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoftPrimes.UI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TourCheckPointsController : _BaseController<TourCheckPoint, TourCheckPointDTO>
    {
        private readonly ITourCheckPointService _TourCheckPointService;
        private readonly IHostingEnvironment _appEnvironment;

        public TourCheckPointsController(ITourCheckPointService businessService, IHelperServices.ISessionServices sessionSevices, IHostingEnvironment appEnvironment) : base(businessService, sessionSevices)
        {
            this._TourCheckPointService = businessService;
            _appEnvironment = appEnvironment;

        }
        [HttpPost("ScanLocationQrCode")]
        public bool ScanLocationQrCode(LocationQrCodeDTO locationQrCode)
        {
            return _TourCheckPointService.ScanLocationQrCode(locationQrCode);
        }
        [HttpGet("CompleteTourCheckPoint")]
        public bool CompleteTourCheckPoint(int TourCheckPointId,int State)
        {
            return _TourCheckPointService.ChangeTourCheckPointState(TourCheckPointId,State);
        }
        [HttpPost("AddCheckPointTourComment")]
        public CommentDTO AddCheckPointTourComment(CheckPointTourCommentDetailDTO checkPointTourComment)
        {
            if (!Request.ContentType.StartsWith("multipart"))
            {
                throw new System.Exception("Invalid multipart request");
            }
            string path = "";
            if (checkPointTourComment.File.Length > 0)
            {
                path = _appEnvironment.WebRootPath + "\\Files\\" + Guid.NewGuid() + "_" + checkPointTourComment.Text;
            }

            System.IO.File.WriteAllBytes(path, checkPointTourComment.File);
            return _TourCheckPointService.AddCheckPointTourComment(checkPointTourComment, path);
        }
    }
}
