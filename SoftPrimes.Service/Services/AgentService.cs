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
   public class AgentService : BusinessService<Agent, AgentDTO>, IAgentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private IBaseRepository<Agent> _repository;
        public AgentService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
            _unitOfWork = unitOfWork;
            _repository = _unitOfWork.GetRepository<Agent>();
        }
        //private void CustomeMethod()
        //{
        //    var company=_repository.
        //}
    }
}