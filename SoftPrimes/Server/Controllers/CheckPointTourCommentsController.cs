using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SoftPrimes.Service.IServices;
using SoftPrimes.Shared.Domains;
using SoftPrimes.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoftPrimes.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CheckPointTourCommentsController : _BaseController<CheckPointTourComment, CheckPointTourCommentDTO>
    {
        private readonly ICheckPointTourCommentService _CheckPointTourCommentService;

        public CheckPointTourCommentsController(ICheckPointTourCommentService businessService, IHelperServices.ISessionServices sessionSevices) : base(businessService, sessionSevices)
        {
            this._CheckPointTourCommentService = businessService;
        }
       
    }
}
