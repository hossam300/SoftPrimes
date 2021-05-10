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
    public class AgentLoginLogsController : _BaseController<AgentLoginLog, AgentLoginLogDTO>
    {
        private readonly IAgentLoginLogService _AgentLoginLogService;

        public AgentLoginLogsController(IAgentLoginLogService businessService) : base(businessService)
        {
            this._AgentLoginLogService = businessService;
        }
        [HttpGet("GetAllAgentLoginLogDTO")]
        public IActionResult GetAllAgentLoginLogDTO()
        {
            var AgentLoginLogDTOs = _AgentLoginLogService.GetAllWithoutInclude().Select(x => new AgentLoginLogDTO
            {
                Id = x.Id,
                AgentId = x.AgentId,
                AccessToken = x.AccessToken,
                MacId = x.MacId,
                MobileType = x.MobileType,
                Network = x.Network,
                SerailNumber = x.SerailNumber
            }).ToList();
            return Ok(AgentLoginLogDTOs);
        }
    }
}
