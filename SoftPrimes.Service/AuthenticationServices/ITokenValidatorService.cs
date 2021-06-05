using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using SoftPrimes.Service.IServices;

namespace IBusinessServices.IAuthenticationServices
{
    public interface ITokenValidatorService : IBusinessService
    {
        Task ValidateAsync(TokenValidatedContext context);
    }
}
