using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SoftPrimes.Service.IServices;
using SoftPrimes.Shared.Domains;
using SoftPrimes.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoftPrimes.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CheckPointTourCommentsController : _BaseController<CheckPointTourComment, CheckPointTourCommentDTO>
    {
        private readonly ICheckPointTourCommentService _CheckPointTourCommentService;

        public CheckPointTourCommentsController(ICheckPointTourCommentService businessService) : base(businessService)
        {
            this._CheckPointTourCommentService = businessService;
        }
        [HttpGet("GetAllCheckPointTourCommentDTO")]
        public IActionResult GetAllCheckPointTourCommentDTO()
        {
            var CheckPointTourCommentDTOs = _CheckPointTourCommentService.GetAllWithoutInclude().Select(x => new CheckPointTourCommentDTO
            {
                Id = x.Id,
                Comment = new CommentDTO { AttachmentId = x.Comment.AttachmentId, Id = x.CommentId, ReplayToComment = x.Comment.ReplayToComment, Text = x.Comment.Text },
                CommentId = x.CommentId,
                TourCheckPoint = new TourCheckPointDTO { CheckPointId = x.TourCheckPoint.CheckPointId, Id = x.TourCheckPointId, TourCheckPointState = x.TourCheckPoint.TourCheckPointState, TourId = x.TourCheckPoint.TourId },
                TourCheckPointId = x.TourCheckPointId,
                CreatedBy = x.CreatedBy,
                CreatedOn = x.CreatedOn
            }).ToList();
            return Ok(CheckPointTourCommentDTOs);
        }
    }
}
