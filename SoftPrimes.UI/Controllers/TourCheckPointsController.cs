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
        public bool CompleteTourCheckPoint(int TourCheckPointId, int State)
        {
            return _TourCheckPointService.ChangeTourCheckPointState(TourCheckPointId, State);
        }
        [HttpPost("AddCheckPointTourComment")]
        public CommentDTO AddCheckPointTourComment()
        {
            CheckPointTourCommentDetailDTO checkPointTourComment = new CheckPointTourCommentDetailDTO();
            string path = "";
            var xx = Request.Form;
            var CheckPointId = Request.Form["CheckPointId"].ToString();
            var Text = Request.Form["Text"].ToString();
            var TourId = Request.Form["TourId"].ToString();
            var AttachmentType = Request.Form["AttachmentType"].ToString();
            var files = Request.Form.Files;
            foreach (var file in files)
            {
                using (var binaryReader = new System.IO.BinaryReader(file.OpenReadStream()))
                {
                    checkPointTourComment.File = binaryReader.ReadBytes((int)file.Length);
                    path = _appEnvironment.WebRootPath + "\\Files\\" + Guid.NewGuid() + "_" + file.FileName;
                    System.IO.File.WriteAllBytes(path, checkPointTourComment.File);
                }
            }
            checkPointTourComment.Text = Text;
            checkPointTourComment.TourId = Convert.ToInt32(TourId);
            checkPointTourComment.CheckPointId = Convert.ToInt32(CheckPointId);
            checkPointTourComment.AttachmentType = Convert.ToInt32(AttachmentType);
            return _TourCheckPointService.AddCheckPointTourComment(checkPointTourComment, path );
        }
    }
}
