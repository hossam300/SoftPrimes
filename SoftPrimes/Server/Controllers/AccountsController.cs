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

namespace SoftPrimes.Server.Controllers
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

     //   UserManager<Agent> _userManager;
        public AccountController(IUsersService usersService, ITokenStoreService tokenStoreService, ISessionServices sessionServices, IHttpContextAccessor httpContextAccessor/*, UserManager<Agent> userManager*/)
        {
            _usersService = usersService;
            _tokenStoreService = tokenStoreService;
            _SessionServices = sessionServices;
            _httpContextAccessor = httpContextAccessor;
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
            return Ok(new AccessToken { access_token = accessToken, refresh_token = refreshToken, is_mobile = true, is_ldap_auth = lsLdapAuth, is_factor_auth = false });
            //}
        }

        [HttpGet("[action]")]
        [ProducesResponseType(200, Type = typeof(AuthTicketDTO))]
        [Authorize]
        public IActionResult GetUserAuthTicket()
        {
            ClaimsIdentity claimsIdentity = User.Identity as ClaimsIdentity;
            string Username = _usersService.Decrypte(claimsIdentity.Name);
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
    }
}
