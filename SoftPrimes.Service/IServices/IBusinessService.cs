using LinqHelper;
using SoftPrimes.BLL.BaseObjects;
using SoftPrimes.Shared.ViewModels;
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
        DataSourceResult<T> GetAll<T>(DataSourceRequest dataSourceRequest, bool WithTracking = true);
        TDetailsDTO GetDetails(object Id, bool WithTracking = true);
        IEnumerable<TDetailsDTO> Insert(IEnumerable<TDetailsDTO> entities);
        IEnumerable<object> Delete(IEnumerable<object> Ids);
        IEnumerable<TDetailsDTO> Update(IEnumerable<TDetailsDTO> Entities);
        bool CheckIfExist(CheckUniqueDTO checkUniqueDTO);
        // bool CheckIfExist(object id);
    }
  
    public interface IBusinessService
    {
    }
}
