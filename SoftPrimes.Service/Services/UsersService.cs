using AutoMapper;
using IHelperServices;
using IHelperServices.Models;
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
using System.IO;
using System.Linq;
using System.Net.Mail;
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
        private readonly IMailServices _mailServices;
        public UsersService(IUnitOfWork unitOfWork, IMapper mapper, ISecurityService securityService, IHttpContextAccessor contextAccessor, IHelperServices.ISessionServices sessionServices, IOptions<AppSettings> appSettings, IDataProtectService dataProtectService, IOptionsSnapshot<BearerTokensOptions> configuration, IMailServices mailServices)
             : base(unitOfWork, mapper, securityService, sessionServices, appSettings)
        {
            _uow = unitOfWork;
            _users = _uow.GetRepository<Agent>();
            _mailServices = mailServices;
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
        private string CreatePassword(int length)
        {

            var upperCase = new char[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
            var lowerCase = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };
            var numbers = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            var rnd = new Random();

            var total = upperCase
                .Concat(lowerCase)
                .Concat(numbers)
                .ToArray();

            var chars = Enumerable
                .Repeat<int>(0, length)
                .Select(i => total[rnd.Next(total.Length)])
                .ToArray();

            var result = new string(chars);
            return result;
        }
        public bool ResetPassword(string Email)
        {
            var user = _UnitOfWork.GetRepository<Agent>().GetAll().FirstOrDefault(c => c.Email.ToUpper() == Email.ToUpper());
            if (user != null)
            {
                var Message = "";
                var mailSubject = "";
                string newPassword = "";

                newPassword = CreatePassword(8);
                user.Password = _securityService.GetSha256Hash(newPassword);
                _uow.SaveChangesAsync();
                getMailResetPasswordMessage(ref Message, ref mailSubject, newPassword);
                _mailServices.SendNotificationEmail(Email, "ContactUs", Message, true, null, null, null);
                return true;
            }
            return false;
        }
        private void getMailResetPasswordMessage(ref string mailMessage, ref string mailSubject, string Password)
        {
            try
            {
                mailSubject = "Reset Password";
                string messageBody = "تم تعديل كلمة المرور ";
                string CreateDate = DateTime.Now.ToString();
                string lblThanks = "كلمة المرور";

                mailMessage = $@"<h2 style='margin-top: 57px;font-size: 17px;'>{mailSubject}</h2>
                                        <div>
                                           <h3>{messageBody}</h3>
                                        </div>
                                    </div>         
                                    <h2 style='margin-top: 57px;font-size: 17px;'>{lblThanks}</h2>
                                        <div>
                                           <h3>{Password}</h3>
                                        </div>
                                     </div>";

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
