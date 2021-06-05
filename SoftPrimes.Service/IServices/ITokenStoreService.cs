using SoftPrimes.Service.IServices;
using SoftPrimes.Shared.Domains;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SoftPrimes.Service.IServices
{
    public interface ITokenStoreService : IBusinessService
    {
        Task AddUserTokenAsync(UserToken userToken);
        Task AddUserTokenAsync(Agent user, string refreshToken, string accessToken, string refreshTokenSource , int ApplicationType);
        Task<bool> IsValidTokenAsync(string accessToken, string userId);
        Task DeleteExpiredTokensAsync();
        Task<UserToken> FindTokenAsync(string refreshToken);
        Task DeleteTokenAsync(string refreshToken);
        Task DeleteTokensWithSameRefreshTokenSourceAsync(string refreshTokenIdHashSource);
        Task InvalidateUserTokensAsync(string userId);
        Task InvalidateUserTokensAsync(string userId, int UserokenId);
        Task<(string accessToken, string refreshToken, IEnumerable<Claim> Claims)> CreateJwtTokens(Agent user, int applicationType, string refreshTokenSource);
        Task RevokeUserBearerTokensAsync(string userIdValue, string refreshToken);
    }
}
