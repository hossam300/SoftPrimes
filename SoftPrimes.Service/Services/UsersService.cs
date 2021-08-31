using AutoMapper;
using IdentityModel;
using IHelperServices;
using IHelperServices.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SoftPrimes.BLL.AuthenticationServices;
using SoftPrimes.BLL.BaseObjects.RepositoriesInterfaces;
using SoftPrimes.Service.IServices;
using SoftPrimes.Shared.Domains;
using SoftPrimes.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Drawing;
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
            string passwordHash = _securityService.GetSha256Hash(password);
            Agent result = await _users.FirstOrDefaultAsync(x => (x.UserName == username || x.Email == username) && x.Password == passwordHash);
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
        public AuthTicketDTO GetUserAuthTicket(string userName)
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
                        BirthDate = AuthUser.BirthDate,
                        CompanyId = AuthUser.CompanyId,
                        JobTitle = AuthUser.JobTitle,
                        Mobile = AuthUser.Mobile,
                        SupervisorId = AuthUser.SupervisorId,
                        Image = AuthUser.Image,
                        LastLoginDate = AuthUser.AgentLoginLogs.LastOrDefault().CreatedOn,
                        AgentRoles = AuthUser.AgentRoles.Select(x => new AgentRoleDTO
                        {
                            AgentId = x.AgentId,
                            Id = x.Id,
                            RoleId = x.RoleId,
                            Role = new RoleDTO
                            {
                                Id = x.Role.Id,
                                RoleNameAr = x.Role.RoleNameAr,
                                RoleNameEn = x.Role.RoleNameEn,
                                Permissions = x.Role.Permissions.Select(y => new RolePermissionDTO
                                {
                                    Id = y.Id,
                                    Permission = new PermissionDTO
                                    {
                                        PermissionNameAr = y.Permission.PermissionNameAr,
                                        PermissionNameEn = y.Permission.PermissionNameEn,
                                        Id = y.PermissionId,
                                        PermissionKey = y.Permission.PermissionKey
                                    },
                                    PermissionId = y.PermissionId,
                                    RoleId = y.RoleId
                                }).ToList()
                            }
                        }).ToList()
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
                New_user.Password = _securityService.GetSha256Hash(user.Password);
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
            var user = _uow.GetRepository<Agent>().GetAll().FirstOrDefault(c => c.Email.ToUpper() == Email.ToUpper());
            if (user != null)
            {
                var Message = "";
                var mailSubject = "";
                string newPassword = "";

                newPassword = CreatePassword(8);
                user.Password = _securityService.GetSha256Hash(newPassword);
                user.TempPassword = true;
                _uow.SaveChangesAsync();
                getMailResetPasswordMessage(ref Message, ref mailSubject, newPassword);
                _mailServices.SendNotificationEmail(Email, "Reset Password", Message, true, null, null, null);
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

        public AgentDTO AddUserImage(string userId, byte[] ProfileImage)
        {

            MemoryStream ms = new MemoryStream(ProfileImage);
            Image newImage = GetReducedImage(100, 100, ms);
            byte[] ProfileImageThumbnail = ImageToByteArray(newImage);
            AgentDTO userDetailsDTO = new AgentDTO();
            Agent currentUser = _uow.GetRepository<Agent>().GetById(userId);
            currentUser.Id = userId;
            currentUser.Image = ProfileImageThumbnail;
            _UnitOfWork.GetRepository<Agent>().Update(currentUser);
            return _Mapper.Map(currentUser, userDetailsDTO);
        }
        public byte[] ImageToByteArray(System.Drawing.Image imageIn)
        {
            MemoryStream ms = new MemoryStream();
            if (imageIn != null)
            {
                imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
            }
            return ms.ToArray();
        }
        public Image GetReducedImage(int width, int height, Stream resourceImage)
        {
            try
            {
                Image image = Image.FromStream(resourceImage);
                Image thumb = image.GetThumbnailImage(width, height, () => false, IntPtr.Zero);

                return thumb;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public IEnumerable<AgentDTO> InsertNewUsers(int roleId, AgentDetailsDTO entity)
        {
            string pass = "";
            pass = entity.Password;
            string PasswordHash = _securityService.GetSha256Hash(entity.Password);
            if (string.IsNullOrEmpty(entity.Password))
            {
                return null;
            }
            entity.Password = PasswordHash;

            List<Agent> users = new List<Agent>();

            Agent New_user = new Agent
            {
                UserName = entity.UserName,
                Password = entity.Password,
                AgentType = entity.AgentType,
                AgentRoles = (roleId == null || roleId == 0) ?
                new List<AgentRole>() :
                new List<AgentRole>()
                {
                    new AgentRole
                    {
                        RoleId=(int)roleId,
                    },
                },
                JobTitle = entity.JobTitle,
                BirthDate = entity.BirthDate,
                CompanyId = entity.CompanyId,
                Email = entity.Email,
                FullNameAr = entity.FullNameAr,
                FullNameEn = entity.FullNameEn,
                SupervisorId = entity.SupervisorId,
                Mobile = entity.Mobile,
                TempPassword = entity.IsTempPassword
            };
            Agent mm = _users.Insert(New_user);
            users.Add(mm);
            return users.Select(u => new AgentDTO { Id = u.Id.ToString(), UserName = u.UserName }).ToList();
        }

        public AgentDTO UpdateUser(int roleId, AgentDetailsDTO oldAgent)
        {
            var OldEntity = this._UnitOfWork.GetRepository<Agent>().GetAll().FirstOrDefault(c => c.Id == oldAgent.Id);
            OldEntity.Email = oldAgent.Email != null ? oldAgent.Email : OldEntity.Email;
            OldEntity.BirthDate = oldAgent.BirthDate != null || oldAgent.BirthDate != DateTime.MinValue ? oldAgent.BirthDate : OldEntity.BirthDate;
            OldEntity.FullNameAr = oldAgent.FullNameAr;
            OldEntity.FullNameEn = oldAgent.FullNameEn;
            OldEntity.JobTitle = oldAgent.JobTitle;
            OldEntity.Mobile = oldAgent.Mobile;
            OldEntity.AgentType = oldAgent.AgentType;
            OldEntity.UserName = oldAgent.UserName != null || oldAgent.UserName != "" ? oldAgent.UserName : OldEntity.UserName;
            OldEntity.CompanyId = oldAgent.CompanyId != null || oldAgent.CompanyId != 0 ? oldAgent.CompanyId : OldEntity.CompanyId;
            OldEntity.SupervisorId = oldAgent.SupervisorId != null || oldAgent.SupervisorId != "" ? oldAgent.SupervisorId : OldEntity.SupervisorId;
            if (oldAgent.Password != "" && oldAgent.Password != null)
            {
                string pass = "";
                pass = oldAgent.Password;
                string PasswordHash = _securityService.GetSha256Hash(oldAgent.Password);
                if (string.IsNullOrEmpty(oldAgent.Password))
                {
                    return null;
                }
                oldAgent.Password = PasswordHash;
            }
            if (roleId != null && roleId != 0)
            {
                this._UnitOfWork.GetRepository<AgentRole>().Delete(OldEntity.AgentRoles);
                OldEntity.AgentRoles.Add(new AgentRole { RoleId = (int)roleId, AgentId = oldAgent.Id });
            }
            this._UnitOfWork.GetRepository<Agent>().Update(OldEntity);
            return _Mapper.Map(OldEntity, typeof(Agent), typeof(AgentDTO)) as AgentDTO;
        }
        public async Task<bool> ChangeTempPassword(string userName, string newPassword)
        {
            try
            {
                var user = this.GetByUserName(userName);
                await this.ChangePasswordForAdminAsync(user.Id, newPassword);
                return true;
            }
            catch (Exception)
            {

                return false;
            }

        }
        public AgentDTO GetByUserName(string Username)
        {
            var User = _users.GetAll(false).FirstOrDefault(x => x.UserName.ToUpper() == Username.ToUpper());

            if (!string.IsNullOrEmpty(Username))
            {
                if (User != null)
                {
                    string UserId = User.Id;
                    if (UserId != null)
                    {
                        AgentDTO user = GetDetails(UserId);
                        return user;
                    }
                }
            }
            return null;
        }
        public async Task<(bool Succeeded, string Error)> ChangePasswordForAdminAsync(string userId, string newPassword)
        {
            Agent user = await FindUserAsync(userId);
            if (user.Id != null)
            {
                user.Password = _securityService.GetSha256Hash(newPassword);
                user.TempPassword = false;
                await _uow.SaveChangesAsync();
                return (true, string.Empty);
            }
            return (false, "User Not Avaliable.");
        }
        public void InsertLoginLog(AgentLoginLog agentLoginLog)
        {
            _uow.GetRepository<AgentLoginLog>().Insert(agentLoginLog);
        }
        public List<AgentDTO> GetAgentLookups(string searchText, int take)
        {
            if (searchText == "" || string.IsNullOrEmpty(searchText) || searchText == null)
            {
                return _users.GetAll().Select(x => new AgentDTO
                {
                    Id = x.Id,
                    AgentType = x.AgentType,
                    FullNameAr = x.FullNameAr,
                    BirthDate = x.BirthDate,
                    CompanyId = x.CompanyId,
                    Email = x.Email,
                    Image = x.Image,
                    JobTitle = x.JobTitle,
                    FullNameEn = x.FullNameEn,
                    Mobile = x.Mobile,
                    UserName = x.UserName,
                    Active = x.Active,
                    SupervisorId = x.SupervisorId
                }).Take(take).ToList();
            }
            else
            {
                return _users.GetAll().Where(x => x.UserName.Contains(searchText) || x.FullNameAr.Contains(searchText) || x.FullNameEn.Contains(searchText))
                    .Select(x => new AgentDTO
                    {
                        Id = x.Id,
                        AgentType = x.AgentType,
                        FullNameAr = x.FullNameAr,
                        BirthDate = x.BirthDate,
                        CompanyId = x.CompanyId,
                        Email = x.Email,
                        Image = x.Image,
                        JobTitle = x.JobTitle,
                        FullNameEn = x.FullNameEn,
                        Mobile = x.Mobile,
                        UserName = x.UserName,
                        Active = x.Active,
                        SupervisorId = x.SupervisorId
                    }).Take(take).ToList();
            }
        }
    }
}
