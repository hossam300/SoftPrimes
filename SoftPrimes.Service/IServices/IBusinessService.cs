using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftPrimes.Service.IServices
{
    public interface IBusinessService<TDbEntity, TDetailsDTO> : IBusinessService
     where TDbEntity : class
    {
        List<T> GetAll<T>();
        List<TDbEntity> GetAllWithoutInclude();
        TDbEntity GetDetails(object Id);
        TDbEntity Insert(TDbEntity entities);
        int Delete(int Ids);
        TDbEntity Update(TDbEntity Entities);
        // bool CheckIfExist(object id);
    }

    public interface IBusinessService
    {
    }
}
