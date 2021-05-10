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
    public class TourCommentsController : _BaseController<TourComment, TourCommentDTO>
    {
        private readonly ITourCommentService _TourCommentService;

        public TourCommentsController(ITourCommentService businessService) : base(businessService)
        {
            this._TourCommentService = businessService;
        }
        [HttpGet("GetAllTourCommentDTO")]
        public IActionResult GetAllTourCommentDTO()
        {
            var TourCommentDTOs = _TourCommentService.GetAllWithoutInclude().Select(x => new TourCommentDTO
            {
                Id = x.Id,
                Comment = new CommentDTO { AttachmentId = x.Comment.AttachmentId, CreatedBy = x.Comment.CreatedBy, CreatedOn = x.Comment.CreatedOn, Id = x.CommentId, ReplayToComment = x.Comment.ReplayToComment, Text = x.Comment.Text },
                CommentId = x.CommentId,
                CreatedBy = x.CreatedBy,
                CreatedOn = x.CreatedOn,
                TourId = x.TourId
            }).ToList();
            return Ok(TourCommentDTOs);
        }
    }
}
