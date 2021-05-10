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
    public class TourAgentsController : _BaseController<TourAgent, TourAgentDTO>
    {
        private readonly ITourAgentService _TourAgentService;

        public TourAgentsController(ITourAgentService businessService) : base(businessService)
        {
            this._TourAgentService = businessService;
        }
        [HttpGet("GetAllTourAgentDTO")]
        public IActionResult GetAllTourAgentDTO()
        {
            var TourAgentDTOs = _TourAgentService.GetAllWithoutInclude().Select(x => new TourAgentDTO
            {
                Id = x.Id,
                AgentId = x.AgentId,
                Agent = new AgentDTO { Active = x.Agent.Active, FullNameAr = x.Agent.FullNameAr, FullNameEn = x.Agent.FullNameEn },
                CheckPoints = x.CheckPoints.Select(y => new TourCheckPointDTO { CheckPoint = new CheckPointDTO { CheckPointNameAr = y.CheckPoint.CheckPointNameAr, CheckPointNameEn = y.CheckPoint.CheckPointNameEn, Lat = y.CheckPoint.Lat, Long = y.CheckPoint.Long, QRCode = y.CheckPoint.QRCode, Id = y.CheckPointId }, }).ToList(),
                Comments = x.Comments.Select(y => new TourCommentDTO { Comment = new CommentDTO { AttachmentId = y.Comment.AttachmentId, Text = y.Comment.Text, ReplayToComment = y.Comment.ReplayToComment, Id = y.CommentId } }).ToList(),
                EstimatedDistance = x.EstimatedDistance,
                EstimatedEndDate = x.EstimatedEndDate,
                TourDate = x.TourDate,
                TourId = x.TourId,
                TourState = x.TourState,
                Tour = new TourDTO { Id = x.TourId, TourNameAr = x.Tour.TourNameAr, TourNameEn = x.Tour.TourNameEn, TourType = x.Tour.TourType }
            }).ToList();
            return Ok(TourAgentDTOs);
        }
    }
}
