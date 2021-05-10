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
    public class AgentLocationLogsController : _BaseController<AgentLocationLog, AgentLocationLogDTO>
    {
        private readonly IAgentLocationLogService _AgentLocationLogService;

        public AgentLocationLogsController(IAgentLocationLogService businessService) : base(businessService)
        {
            this._AgentLocationLogService = businessService;
        }
        [HttpGet("GetAllAgentLocationLogDTO")]
        public IActionResult GetAllAgentLocationLogDTO()
        {
            var AgentLocationLogDTOs = _AgentLocationLogService.GetAllWithoutInclude().Select(x => new AgentLocationLogDTO { Id = x.Id, AgentId = x.AgentId, Lat = x.Lat, Long = x.Long }).ToList();
            return Ok(AgentLocationLogDTOs);
        }
    }
}
