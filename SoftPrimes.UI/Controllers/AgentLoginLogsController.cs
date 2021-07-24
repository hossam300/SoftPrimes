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
    public class AgentLoginLogsController : _BaseController<AgentLoginLog, AgentLoginLogDTO>
    {
        private readonly IAgentLoginLogService _AgentLoginLogService;

        public AgentLoginLogsController(IAgentLoginLogService businessService, IHelperServices.ISessionServices sessionSevices) : base(businessService, sessionSevices)
        {
            this._AgentLoginLogService = businessService;
        }
      
    }
}
