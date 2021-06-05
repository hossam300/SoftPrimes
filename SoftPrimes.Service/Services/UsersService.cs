using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SoftPrimes.BLL.AuthenticationServices;
using SoftPrimes.BLL.BaseObjects.RepositoriesInterfaces;
using SoftPrimes.Service.IServices;
using SoftPrimes.Shared.Domains;
using SoftPrimes.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SoftPrimes.Service.Services
{
    public class UsersService : BusinessService<Agent, AgentDTO>, IUsersService
    {
        private readonly IUnitOfWork _uow;
        private readonly IRepository<Agent> _users;
        private readonly ISecurityService _securityService;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IDataProtectService _dataProtectService;
        private readonly IHelperServices.ISessionServices _sessionServices;
        private readonly IOptionsSnapshot<BearerTokensOptions> _configuration;
        public UsersService(IUnitOfWork unitOfWork, IMapper mapper, ISecurityService securityService, IHttpContextAccessor contextAccessor, IHelperServices.ISessionServices sessionServices, IOptions<AppSettings> appSettings, IDataProtectService dataProtectService, IOptionsSnapshot<BearerTokensOptions> configuration)
             : base(unitOfWork, mapper, securityService, sessionServices, appSettings)
        {
            _uow = unitOfWork;
            _users = _uow.GetRepository<Agent>();

            _securityService = securityService;
            _contextAccessor = contextAccessor;
            _sessionServices = sessionServices;
            _dataProtectService = dataProtectService;
            _configuration = configuration;
        }

        public async Task<Agent> FindUserAsync(string userId)
        {
            return await _users.FindAsync(userId);
        }

        public async Task<Agent> FindUserPasswordAsync(string username, string password, bool isHashedPassword)
        {

            Agent result = await _users.FirstOrDefaultAsync(x => x.UserName == username);
            return result;

        }

        public Task<Agent> GetCurrentUserAsync()
        {
            string userId = GetCurrentUserId();
            return FindUserAsync(userId);
        }

        public string GetCurrentUserId()
        {
            ClaimsIdentity claimsIdentity = _contextAccessor.HttpContext.User.Identity as ClaimsIdentity;
            Claim userDataClaim = claimsIdentity?.FindFirst(ClaimTypes.UserData);
            string userId = userDataClaim?.Value;
            return userId;
        }

        public async Task<string> GetSerialNumberAsync(string userId)
        {
            Agent user = await FindUserAsync(userId);
            return user.PhoneNumber;
        }
        public string Decrypte(string EncrypteString)
        {
            return _dataProtectService.Decrypt(EncrypteString);
        }
        public string Encrypt(string EncrypteString)
        {
            return _dataProtectService.Encrypt(EncrypteString);
        }
        public AuthTicketDTO GetUserAuthTicket(string userName, int? organizationId = null, int? roleId = null, bool? personal = false)
        {
            try
            {
                bool IsArabic = _sessionServices.CultureIsArabic;
                Agent AuthUser = _users.GetAll().FirstOrDefault(x => x.UserName == userName);
                if (AuthUser != null)
                {
                    AuthTicketDTO Result = new AuthTicketDTO()
                    {
                        Email = AuthUser.Email,
                        //FullName = IsArabic ? AuthUser.FullNameAr : AuthUser.FullNameEn,
                        FullNameAr = AuthUser.FullNameAr,
                        FullNameEn = AuthUser.FullNameEn,
                        UserName = AuthUser.UserName,
                        UserId = AuthUser.Id.ToString(),
                        FullName = AuthUser.FullNameAr,

                    };
                    return Result;
                }
                return null;
            }
            catch (Exception ex)
            {
                //if (System.IO.File.Exists("C:\\testemail.txt"))
                //{
                //    using (StreamWriter sw = System.IO.File.AppendText("C:\\testemail.txt"))
                //    {
                //        sw.WriteLine("function= GetUserAuthTicket");
                //        sw.WriteLine("userName= " + userName);
                //        sw.WriteLine("Mes=" + ex.Message);
                //        sw.WriteLine("Stack Trace=" + ex.StackTrace);
                //    }
                //}
                return null;
            }
        }

        public override IEnumerable<AgentDTO> Insert(IEnumerable<AgentDTO> entities)
        {
            string pass = "";
            List<Agent> users = new List<Agent>();
            foreach (AgentDTO user in entities)
            {
                Agent New_user = _Mapper.Map(user, typeof(AgentDTO), typeof(Agent)) as Agent;
                Agent mm = _users.Insert(New_user);
                users.Add(mm);
            }
            return _Mapper.Map(users, typeof(IEnumerable<Agent>), typeof(IEnumerable<AgentDTO>)) as IEnumerable<AgentDTO>;
        }



    }
}
