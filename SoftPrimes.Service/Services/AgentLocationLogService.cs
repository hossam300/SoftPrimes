using AutoMapper;
using SoftPrimes.BLL.BaseObjects.RepositoriesInterfaces;
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
    public class AgentLocationLogService : BusinessService<AgentLocationLog, AgentLocationLogDTO>, IAgentLocationLogService
    {
        private readonly IUnitOfWork _unitOfWork;
        private IRepository<AgentLocationLog> _repository;
        public AgentLocationLogService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
            _unitOfWork = unitOfWork;
            _repository = _unitOfWork.GetRepository<AgentLocationLog>();
        }
        //private void CustomeMethod()
        //{
        //    var company=_repository.
        //}
    }
}