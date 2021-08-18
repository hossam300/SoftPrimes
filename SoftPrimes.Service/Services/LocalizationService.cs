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
    public class LocalizationService : BusinessService<Localization, LocalizationDTO>, ILocalizationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private IRepository<Localization> _repository;
        public LocalizationService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
            _unitOfWork = unitOfWork;
            _repository = _unitOfWork.GetRepository<Localization>();
        }
        public string GetJson(string culture)
        {
            //var list = _mainDbContext.Localizations.Include(x => x.LocalizationCategory).AsNoTracking().ToList();
            //var result = "{" + string.Join(", ", list.GroupBy(x => x.LocalizationCategory).Select(x => "\"" + x.Key.Code + "\":{" + string.Join(", ", x.Select(s => "\"" + s.Key + "\":\"" + s.Value + "\"")) + "}")) + "}";
            var list = _UnitOfWork.GetRepository<Localization>()
                .GetAll(false)
                .Select(x => new {
                    x.Key,
                    Value = culture.ToLower().StartsWith("ar") ? x.ValueAr
                : culture.ToLower().StartsWith("en") ? x.ValueEn
                : x.ValueAr
                })
                .ToList();
            var result = "{" + string.Join(", ", list.Select(x => "\"" + x.Key + "\":\"" + x.Value + "\"")) + "}";
            //StringBuilder resultx = new StringBuilder("{");
            //foreach (var x in list)
            //{
            //    resultx.Append('"').Append(x.Key.TrimEnd().TrimStart()).Append('"').Append(":").Append('"').Append(x.Value.TrimEnd().TrimStart()).Append('"').Append(",");
            //}
            //var xx = resultx.Append("}").ToString();
            //var result = xx.Remove(xx.LastIndexOf(","), 1);

            //return JsonConvert.SerializeObject(result);
            return result;
        }

        public DateTime GetLastLocalizationUpdateTime()
        {
            return DateTime.Parse(_UnitOfWork.GetRepository<Localization>().GetAll(false).Select(x => x.CreatedOn)
                .Union(_UnitOfWork.GetRepository<Localization>().GetAll(false).Select(x => x.UpdatedOn)).Max().GetValueOrDefault().ToString());
        }
        //private void CustomeMethod()
        //{
        //    var Localization=_repository.
        //}
    }
}