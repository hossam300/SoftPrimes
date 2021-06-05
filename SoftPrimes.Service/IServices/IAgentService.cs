using SoftPrimes.Shared.Domains;
using SoftPrimes.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftPrimes.Service.IServices
{
    public interface IAgentService : IBusinessService<Agent, AgentDTO>
    {
        IEnumerable<AgentDTO> InsertNewUsers(IEnumerable<AgentDTO> entities);
        IEnumerable<AgentDTO> UpdateUsers(IEnumerable<AgentDTO> entities);
        string ModifyProfileImages();
        AgentDTO AddUserImage(string userId, byte[] profileImage);
        AgentDTO GetByUserName(string username, bool v);
    }
}
