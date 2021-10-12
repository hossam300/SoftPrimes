using SoftPrimes.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftPrimes.Service.IServices
{
    public interface IDashbordService
    {
        List<PiChartDTO> TourStatus(DateTime? start, DateTime? end);
        TourVsMontringDate TourMontringVsDate(DateTime? start, DateTime? end);
        List<PiChartDTO> CheckPointCount(DateTime? start, DateTime? end);
    }
}
