using Microsoft.AspNetCore.Authorization;
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
    public class AgentsController : _BaseController<Agent, AgentDTO>
    {
        private readonly IAgentService _agentService;

        public AgentsController(IAgentService businessService, IHelperServices.ISessionServices sessionSevices) : base(businessService, sessionSevices)
        {
            this._agentService = businessService;
        }
        [HttpPost, Route("InsertNewAgent")]
        [AllowAnonymous]
        public IEnumerable<object> InsertNewUsers([FromBody] IEnumerable<AgentDTO> entities)
        {
            return _agentService.InsertNewUsers(entities);
        }

        [HttpPut, Route("UpdateUsers")]
        public IEnumerable<AgentDTO> UpdateUsers([FromBody] IEnumerable<AgentDTO> entities)
        {
            return _agentService.UpdateUsers(entities);
        }

        [HttpGet("ModifyProfileImages")]
        [AllowAnonymous]
        public string ModifyProfileImages() { return _agentService.ModifyProfileImages(); }

        [HttpPost("AddUserImage")]
        public object AddUserImage(string userId)
        {

            if (!Request.ContentType.StartsWith("multipart"))
            {
                throw new System.Exception("Invalid multipart request");
            }
            Microsoft.AspNetCore.Http.IFormFile file = Request.Form.Files[0];
            byte[] BinaryContent = null;
            using (System.IO.BinaryReader binaryReader = new System.IO.BinaryReader(file.OpenReadStream()))
            {
                BinaryContent = binaryReader.ReadBytes((int)file.Length);
            }
            byte[] ProfileImage = BinaryContent;
            string ProfileImageMimeType = file.ContentType;
            var user = _agentService.AddUserImage(userId, ProfileImage);
            return new { UserId = user.Id, ProfileImage = user.Image };
        }
        [AllowAnonymous]
        [HttpGet]
        [Route("GetUserByUserName")]
        [ProducesResponseType(200, Type = typeof(AgentDTO))]
        public IActionResult GetUserByUserName(string username)
        {

            AgentDTO UserDetails = _agentService.GetByUserName(username, false);
            if (UserDetails != null)
            {
                return Ok(UserDetails);
            }
            return null;
        }
        [HttpGet]
        [Route("GetByUserName")]
        [ProducesResponseType(200, Type = typeof(AgentDTO))]
        public IActionResult GetByUserName(string username)
        {
            if (TempData["EditFromList"] != null)
            {
                AgentDTO UserDetails = _agentService.GetByUserName(username, true);
                if (UserDetails != null)
                {
                    //UserDetails.UserIdEncrypt = _dataProtectService.Encrypt(UserDetails.UserId.ToString());
                    //TempData["EditFromList"] = null;
                    return Ok(UserDetails);

                }

                return NotFound();
            }
            return NotFound();
        }
       
    }
}
