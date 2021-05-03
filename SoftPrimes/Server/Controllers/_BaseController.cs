using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SoftPrimes.Service.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoftPrimes.Server.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class _BaseController : ControllerBase
    {
        protected readonly IBusinessService _BusinessService;
        private IWebHostEnvironment env;
        public _BaseController(IBusinessService businessService)
        {
            this._BusinessService = businessService;
        }
        public _BaseController(IBusinessService businessService, IWebHostEnvironment env)
        {
            this._BusinessService = businessService;
            this.env = env;
        }
    }

    [Route("api/[controller]")]
    public class _BaseController<TDbEntity, TDetailsDTO> : Controller where TDbEntity : class
    {
        protected readonly IBusinessService<TDbEntity, TDetailsDTO> _BusinessService;
        private IWebHostEnvironment env;

        public _BaseController(IBusinessService<TDbEntity, TDetailsDTO> businessService)
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
        [HttpGet, Route("GetAll")]
        public virtual IActionResult GetAll()
        {
            return Ok(this._BusinessService.GetAll<TDbEntity>());
        }

        /// <summary>
        /// البحث بالكود
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet, Route("GetById/{id}")]
        public virtual IActionResult Get(int id)
        {
            return Ok(this._BusinessService.GetDetails(id));
        }

        /// <summary>
        /// إدخال
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost, Route("Insert")]
        public virtual IActionResult Post([FromBody] TDbEntity entities)
        {
            var entity = this._BusinessService.Insert(entities);
            return Ok(entity);
        }

        /// <summary>
        /// تحديث
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        [HttpPut, Route("Update")]
        public virtual IActionResult Put([FromBody] TDbEntity entities)
        {
            return Ok(this._BusinessService.Update(entities));
        }

        /// <summary>
        /// حذف
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpDelete, Route("Delete/{id}")]
        public virtual IActionResult Delete(int id)
        {
            return Ok(this._BusinessService.Delete(id));
        }


    }
}
