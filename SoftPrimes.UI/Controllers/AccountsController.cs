using IHelperServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SoftPrimes.Service.IServices;
using SoftPrimes.Shared.Domains;
using SoftPrimes.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SoftPrimes.UI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AccountController : ControllerBase
    {
        private readonly IUsersService _usersService;
        private readonly ITokenStoreService _tokenStoreService;
        private readonly ISessionServices _SessionServices;
        //private readonly IAntiForgeryCookieService _antiforgery;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMailServices _mailServices;
        //   UserManager<Agent> _userManager;
        public AccountController(IUsersService usersService, ITokenStoreService tokenStoreService, ISessionServices sessionServices, IHttpContextAccessor httpContextAccessor, IMailServices mailServices/*, UserManager<Agent> userManager*/)
        {
            _usersService = usersService;
            _tokenStoreService = tokenStoreService;
            _SessionServices = sessionServices;
            _httpContextAccessor = httpContextAccessor;
            _mailServices = mailServices;
            //   _userManager = userManager;
        }
        [AllowAnonymous]
        //[IgnoreAntiforgeryToken]
        [HttpPost("[action]")]
        [ProducesResponseType(200, Type = typeof(AccessToken))]
        public async Task<IActionResult> Login([FromBody] UserLoginModel loginUser, string culture)
        {

            Agent _User = null;
            bool lsLdapAuth = true;
            //   bool isFactorAuth = false;

            //SET CULTURE SESSION
            _SessionServices.Culture = culture;

            //SET ApplicationType SESSION
            _SessionServices.ApplicationType = loginUser.applicationType;

            _User = await _usersService.FindUserPasswordAsync(loginUser.Username, loginUser.Password, false);

            if (_User == null)
            {
                return Ok(new AccessToken { access_token = null, refresh_token = null, is_ldap_auth = lsLdapAuth, IsAdmin = false, IsTemp = false });
            }
            //  var pass =await _userManager.CheckPasswordAsync(_User, loginUser.Password);
            //if (!pass)
            //{
            //    return Ok(new AccessToken { access_token = null, refresh_token = null, is_ldap_auth = lsLdapAuth, IsAdmin = false, IsTemp = false });
            //}
            int applicationType = int.Parse(loginUser.applicationType);

            //_antiforgery.RegenerateAntiForgeryCookies(claims);
            var (accessToken, refreshToken, claims) = await _tokenStoreService.CreateJwtTokens(
                _User,
                applicationType,
                refreshTokenSource: null);
            return Ok(new AccessToken { access_token = accessToken, refresh_token = refreshToken, IsTemp = _User.TempPassword, is_mobile = true, is_ldap_auth = lsLdapAuth, is_factor_auth = false });
            //}
        }

        [HttpGet("[action]")]
        [ProducesResponseType(200, Type = typeof(AuthTicketDTO))]
        [Authorize]
        public IActionResult GetUserAuthTicket()
        {
            ClaimsIdentity claimsIdentity = User.Identity as ClaimsIdentity;
            string Username = claimsIdentity.Name;
            AuthTicketDTO AuthTicket = this._usersService.GetUserAuthTicket(Username);
            return Ok(AuthTicket != null ? AuthTicket : null);
        }
        [AllowAnonymous]
        [HttpGet("[action]")]
        [ProducesResponseType(200, Type = typeof(bool))]
        public async Task<bool> Logout(string refreshToken)
        {
            ClaimsIdentity claimsIdentity = this.User.Identity as ClaimsIdentity;
            string userIdValue = claimsIdentity.FindFirst(ClaimTypes.UserData)?.Value;

            // The Jwt implementation does not support "revoke OAuth token" (logout) by design.
            // Delete the user's tokens from the database (revoke its bearer token)
            await _tokenStoreService.RevokeUserBearerTokensAsync(userIdValue, refreshToken);
            string[] ExecptParm = new string[] { };
            _SessionServices.ClearSessionsExcept(ExecptParm);
            return true;
        }
        [AllowAnonymous]
        [HttpPost("[action]")]
        [ProducesResponseType(200, Type = typeof(bool))]
        public bool ContactUs(MessageDTO message)
        {
            try
            {
                _mailServices.SendNotificationEmail(message.Email, "ContactUs", message.Message, false, null, null, null);
                return true;
            }
            catch (Exception)
            {

                return false;
            }

        }
        [Authorize]
        [HttpGet("[action]")]
        [ProducesResponseType(200, Type = typeof(AgentDTO))]
        public AgentDTO GetUserProfile(string userId)
        {
            return _usersService.GetDetails(userId);
        }
        [AllowAnonymous]
        [HttpGet("[action]")]
        public bool ResetPassword(string Email)
        {
            Email = Email.Replace("%40", "@");
            return _usersService.ResetPassword(Email);
        }
        [HttpPost, Route("InsertNewUsers")]
        public IEnumerable<AgentDTO> InsertNewUsers([FromBody] IEnumerable<AgentDTO> entities)
        {
            return _usersService.InsertNewUsers(entities);
        }

        [HttpPut, Route("UpdateUsers")]
        public AgentDTO UpdateUser([FromBody] AgentDetailsDTO entity)
        {
            return this._usersService.UpdateUser(entity);
        }
        [HttpPost("AddUserImage")]
        public object AddUserImage(string userId)
        {
            string DecryptedUserId = userId;

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
            var user = _usersService.AddUserImage(DecryptedUserId, ProfileImage);
            return new { UserId = user.Id, ProfileImage = user.Image };
        }
        [AllowAnonymous]
        [HttpGet("[action]")]
        public async Task<bool> ChangeTempPassword(string userName, string newPassword)
        {
            return await _usersService.ChangeTempPassword(userName, newPassword);
        }
    }
}
