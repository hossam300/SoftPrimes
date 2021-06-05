using IHelperServices;
using LinqHelper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SoftPrimes.Service.IServices;
using SoftPrimes.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoftPrimes.Server.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class _BaseController<TDbEntity, TDetailsDTO> : Controller where TDbEntity : class
    {
        protected readonly IBusinessService<TDbEntity, TDetailsDTO> _BusinessService;
        private IWebHostEnvironment env;
        private ISessionServices sessionSevices;

        public _BaseController(IBusinessService<TDbEntity, TDetailsDTO> businessService, IHelperServices.ISessionServices sessionSevices)
        {
            this._BusinessService = businessService;
        }

        public _BaseController(IBusinessService<TDbEntity, TDetailsDTO> businessService, IWebHostEnvironment env)
        {
            this._BusinessService = businessService;
            this.env = env;
        }

      

        /// <summary>
        /// Get All
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAll")]
        public virtual DataSourceResult<TDetailsDTO> GetAll([FromQuery] DataSourceRequest dataSourceRequest)
        {
            var x = this._BusinessService.GetAll<TDetailsDTO>(dataSourceRequest, false);
            return x;
        }

        /// <summary>
        /// البحث بالكود
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("GetAllDetails")]
        public virtual DataSourceResult<TDetailsDTO> GetAllDetails([FromQuery] DataSourceRequest dataSourceRequest)
        {
            return this._BusinessService.GetAll<TDetailsDTO>(dataSourceRequest, false);
        }
        [HttpGet("GetById")]
        public virtual TDetailsDTO Get(string id)
        {
            return _BusinessService.GetDetails(id, true);
        }
        /// <summary>
        /// إدخال
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost("Insert")]
        public virtual IEnumerable<TDetailsDTO> Post([FromBody] IEnumerable<TDetailsDTO> entities)
        {
            return _BusinessService.Insert(entities);
        }

        /// <summary>
        /// تحديث
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        [HttpPut("Update")]
        public virtual IEnumerable<TDetailsDTO> Put([FromBody] IEnumerable<TDetailsDTO> entities)
        {
            return this._BusinessService.Update(entities);
        }

        /// <summary>
        /// حذف
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpDelete("Delete/{id}")]
        public virtual IEnumerable<object> Delete(int id)
        {
            IEnumerable<object> ids = new List<object>() { id };
            try
            {
                var newids = this._BusinessService.Delete(ids);

                return newids;
            }
            catch (Exception ex)
            {
                return null;
            }

        }
        [HttpPost("IsExisted")]
        public virtual bool CheckIfExist([FromBody] CheckUniqueDTO checkUniqueDTO)
        {
            return this._BusinessService.CheckIfExist(checkUniqueDTO);
        }
        //[HttpDelete("DeleteForMob")]
        //public virtual IEnumerable<object> DeleteForMob([FromBody] Delete_objectDTO Delete_object)
        //{

        //    return this._BusinessService.Delete(Delete_object.ids);
        //}

    }
}
