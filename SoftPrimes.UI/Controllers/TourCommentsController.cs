using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SoftPrimes.Service.IServices;
using SoftPrimes.Shared.Domains;
using SoftPrimes.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoftPrimes.UI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TourCommentsController : _BaseController<TourComment, TourCommentDTO>
    {
        private readonly ITourCommentService _TourCommentService;

        public TourCommentsController(ITourCommentService businessService, IHelperServices.ISessionServices sessionSevices) : base(businessService, sessionSevices)
        {
            this._TourCommentService = businessService;
        }
       
    }
}
