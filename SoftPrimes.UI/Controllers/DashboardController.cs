﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SoftPrimes.Service.IServices;
using SoftPrimes.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoftPrimes.UI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly IDashbordService _dashbordService;
        public DashboardController(IDashbordService dashbordService)
        {
            _dashbordService = dashbordService;
        }
        [HttpGet("TourStatus")]
        public List<PiChartDTO> TourStatus(DateTime? start,DateTime? end)
        {
            return _dashbordService.TourStatus(start, end);
        }
        [HttpGet("TourMontringVsDate")]
        public TourVsMontringDate TourMontringVsDate(DateTime? start, DateTime? end)
        {
            return _dashbordService.TourMontringVsDate(start, end);
        }
        [HttpGet("CheckPointCount")]
        public List<PiChartDTO> CheckPointCount(DateTime? start, DateTime? end)
        {
            return _dashbordService.CheckPointCount(start, end);
        }
        [HttpGet("AgentDistance")]
        public List<PiChartDTO> AgentDistance(DateTime? start, DateTime? end)
        {
            return _dashbordService.AgentDistance(start, end);
        }

        [HttpGet("OverDue")]
        public List<LineChartWithdate> OverDue(DateTime? start, DateTime? end)
        {
            return _dashbordService.OverDue(start, end);
        }
    }
}
