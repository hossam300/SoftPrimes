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
    public class AgentsController : _BaseController<Agent, AgentDTO>
    {
        private readonly IAgentService _agentService;

        public AgentsController(IAgentService businessService) : base(businessService)
        {
            this._agentService = businessService;
        }
        [HttpGet("GetAllAgentDTO")]
        public IActionResult GetAllAgentDTO()
        {
            var AgentDTOs = _agentService.GetAllWithoutInclude().Select(x => new AgentDTO { Id = x.Id, UserName = x.UserName, FullNameAr = x.FullNameAr, FullNameEn = x.FullNameEn }).ToList();
            return Ok(AgentDTOs);
        }
    }
}
