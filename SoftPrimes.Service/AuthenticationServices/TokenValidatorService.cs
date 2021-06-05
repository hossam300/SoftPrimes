using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using HelperServices;
using IBusinessServices;
using IBusinessServices.IAuthenticationServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using SoftPrimes.Service.IServices;

namespace BusinessServices.AuthenticationServices
{
    public class TokenValidatorService : ITokenValidatorService
    {
        private readonly IUsersService _usersService;
        private readonly ITokenStoreService _tokenStoreService;

        public TokenValidatorService(IUsersService usersService, ITokenStoreService tokenStoreService)
        {
            _usersService = usersService;
            // _usersService.CheckArgumentIsNull(nameof(usersService));

            _tokenStoreService = tokenStoreService;
            // _tokenStoreService.CheckArgumentIsNull(nameof(_tokenStoreService));
        }

        public async Task ValidateAsync(TokenValidatedContext context)
        {
            var userPrincipal = context.Principal;

            var claimsIdentity = context.Principal.Identity as ClaimsIdentity;
            if (claimsIdentity?.Claims == null || !claimsIdentity.Claims.Any())
            {
                context.Fail("This is not our issued token. It has no claims.");
                return;
            }

            var serialNumberClaim = claimsIdentity.FindFirst(ClaimTypes.SerialNumber);
            if (serialNumberClaim == null)
            {
                context.Fail("This is not our issued token. It has no serial.");
                return;
            }

            var userIdString = claimsIdentity.FindFirst(ClaimTypes.UserData).Value;
            

            var user = await _usersService.FindUserAsync(userIdString);
            if (user == null || !user.Active)
            {
                // user has changed his/her password/roles/stat/IsActive
                context.Fail("This token is expired. Please login again.");
            }

            var accessToken = context.SecurityToken as JwtSecurityToken;
            if (accessToken == null || string.IsNullOrWhiteSpace(accessToken.RawData) ||
                !await _tokenStoreService.IsValidTokenAsync(accessToken.RawData, user.Id))
            {
                context.Fail("This token is not in our database.");
                return;
            }

            await _usersService.UpdateUserLastActivityDateAsync(user.Id);
        }
    }
}