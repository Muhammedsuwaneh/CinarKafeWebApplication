using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CinarKafe.Models.ViewModels
{
    public class HomeViewModel
    {
        public PieChartViewModel PieChartData { get; set; }

        public BarChartViewModel BarChartData { get; set; }

        public Dictionary<Tuple<string, string>, int> GridViewData { get; set; } 
    }
}