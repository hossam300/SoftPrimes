using SoftPrimes.Shared.Domains;
using SoftPrimes.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftPrimes.Service.IServices
{
    public interface IUsersService : IBusinessService<Agent, AgentDTO>
    {
        Task<string> GetSerialNumberAsync(string userId);
        Task<Agent> FindUserPasswordAsync(string username, string password, bool isHashedPassword);
        Task<Agent> FindUserAsync(string userId);
        Task<Agent> GetCurrentUserAsync();
        new IEnumerable<object> Delete(IEnumerable<object> Ids);
        //  UserDetailsDTO GetIndividualUserDetails(string username, bool IsEncrypt);
        string GetCurrentUserId();
        string Decrypte(string name);
        AuthTicketDTO GetUserAuthTicket(string userName);
        bool ResetPassword(string email);
        AgentDTO AddUserImage(string decryptedUserId, byte[] profileImage);
        IEnumerable<AgentDTO> InsertNewUsers(IEnumerable<AgentDTO> entities);
        AgentDTO UpdateUser(AgentDetailsDTO entitie);
        Task<bool> ChangeTempPassword(string userName, string newPassword);
        void InsertLoginLog(AgentLoginLog agentLoginLog);
        List<AgentDTO> GetAgentLookups(string searchText);
    }
}
