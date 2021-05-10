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
    public class AttachmentsController : _BaseController<Attachment, AttachmentDTO>
    {
        private readonly IAttachmentService _AttachmentService;

        public AttachmentsController(IAttachmentService businessService) : base(businessService)
        {
            this._AttachmentService = businessService;
        }
        [HttpGet("GetAllAttachmentDTO")]
        public IActionResult GetAllAttachmentDTO()
        {
            var AttachmentDTOs = _AttachmentService.GetAllWithoutInclude().Select(x => new AttachmentDTO { Id = x.Id, AttachmentName = x.AttachmentName, AttachmentType = x.AttachmentType, AttachmentUrl = x.AttachmentUrl }).ToList();
            return Ok(AttachmentDTOs);
        }
    }
}
