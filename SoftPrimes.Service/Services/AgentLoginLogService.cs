using AutoMapper;
using SoftPrimes.BLL.BaseObjects.ReSoftPrimesitoriesInterfaces;
using SoftPrimes.Service.IServices;
using SoftPrimes.Shared.Domains;
using SoftPrimes.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftPrimes.Service.Services
{
    public class AgentLoginLogService : BusinessService<AgentLoginLog, AgentLoginLogDTO>, IAgentLoginLogService
    {
        private readonly IUnitOfWork _unitOfWork;
        private IBaseRepository<AgentLoginLog> _repository;
        public AgentLoginLogService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
            _unitOfWork = unitOfWork;
            _repository = _unitOfWork.GetRepository<AgentLoginLog>();
        }
        //private void CustomeMethod()
        //{
        //    var AgentLoginLog=_repository.
        //}
    }
}