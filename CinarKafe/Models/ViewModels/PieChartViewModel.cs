using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CinarKafe.Models.ViewModels
{
    public class PieChartViewModel
    {
        public List<string> labels { get; set; }

        public List<PieChartChildViewModel> datasets { get; set; }

        public PieChartViewModel()
        {
            labels = new List<string>();

            datasets = new List<PieChartChildViewModel>();
        }
    }
}