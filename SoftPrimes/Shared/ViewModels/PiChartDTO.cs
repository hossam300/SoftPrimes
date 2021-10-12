using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftPrimes.Shared.ViewModels
{
    public class PiChartDTO
    {
        public double Value { get; set; }
        public string Text { get; set; }
    }
    public class TourVsMontringDate
    {
        public List<LineChartWithdate> MontringVsDate { get; set; }
        public List<LineChartWithdate> TourVsDate { get; set; }

    }
    public class LineChartWithdate
    {
        public string Date { get; set; }
        public double Value { get; set; }
    }
}
