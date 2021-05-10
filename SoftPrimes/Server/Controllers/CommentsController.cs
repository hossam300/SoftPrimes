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
    public class CommentsController : _BaseController<Comment, CommentDTO>
    {
        private readonly ICommentService _CommentService;

        public CommentsController(ICommentService businessService) : base(businessService)
        {
            this._CommentService = businessService;
        }
        [HttpGet("GetAllCommentDTO")]
        public IActionResult GetAllCommentDTO()
        {
            var CommentDTOs = _CommentService.GetAllWithoutInclude().Select(x => new CommentDTO
            {
                Id = x.Id,
                AttachmentId = x.AttachmentId,
                ReplayToComment = x.ReplayToComment,
                Text = x.Text,
                Attachment = new AttachmentDTO { AttachmentName = x.Attachment.AttachmentName, AttachmentType = x.Attachment.AttachmentType, AttachmentUrl = x.Attachment.AttachmentUrl, CreatedBy = x.Attachment.CreatedBy, CreatedOn = x.Attachment.CreatedOn, Id = x.Attachment.Id },
                CreatedOn = x.CreatedOn,
                CreatedBy = x.CreatedBy
            }).ToList();
            return Ok(CommentDTOs);
        }
    }
}
