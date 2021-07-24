using IHelperServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using SoftPrimes.BLL.Contexts;
using SoftPrimes.Service.IServices;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace SoftPrimes.UI
{
    public class DbStringLocalizer : IStringLocalizer, IHttpContextAccessor
    {
        protected readonly ApplicationDbContext _DbContext;
        protected readonly IHttpContextAccessor _HttpContextAccessor;
        protected readonly ISessionServices _SessionServices;

        public DbStringLocalizer(ApplicationDbContext dbContext, IHttpContextAccessor httpContextAccessor, ISessionServices sessionServices)
        {
            _DbContext = dbContext;
            _HttpContextAccessor = httpContextAccessor;
            _SessionServices = sessionServices;

        }

        public LocalizedString this[string name]
        {
            get
            {
                return Translate(name);
            }
        }

        public LocalizedString this[string name, params object[] arguments]
        {
            get
            {
                return Translate(name, arguments);
            }
        }

        public HttpContext HttpContext
        {
            get
            {
                return _HttpContextAccessor.HttpContext;
            }

            set
            {
                _HttpContextAccessor.HttpContext = value;
            }
        }

        public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
        {
            throw new NotImplementedException();
        }

        private LocalizedString Translate(string name, params object[] arguments)
        {
            //var splittedName = name.Split('.');
            //var category = splittedName.Length >= 2 ? splittedName[0] : null;
            //var key = splittedName.Length >= 2 ? splittedName[1] : null;

            var localizer = _DbContext.Localizations.AsNoTracking().FirstOrDefault(x => x.Key == name);
            var value = localizer != null ? (_SessionServices.CultureIsArabic ? localizer.ValueAr : localizer.ValueEn) : name;

            var resourceNotFound = string.IsNullOrEmpty(value);
            value = value ?? name;
            if (arguments.Length > 0)
            {
                var culture = CultureInfo.CurrentCulture;
                var calendar = HttpContext.Request.Cookies["DefaultCalendar"];
                if (!string.IsNullOrEmpty(calendar))
                {
                    //if (CultureInfo.CurrentCulture.TwoLetterISOLanguageName == "ar")
                    //{
                    //if (calendar.ToLower() == "ummalqura")
                    //    culture = new CultureInfo("ar-SA");
                    //else
                    //    culture = new CultureInfo("ar-EG");
                    //}
                }
                value = string.Format(culture, value, arguments);
            }
            return new LocalizedString(name, value, resourceNotFound);
        }
    }
}
