using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CinarKafe.Models.ViewModels
{
    public class BarChartViewModel
    {
        public List<string> labels { get; set; }

        public List<BarChartChildViewModel> datasets { get; set; }

        public BarChartViewModel()
        {
            labels = new List<string>();

            datasets = new List<BarChartChildViewModel>();
        }
    }
}