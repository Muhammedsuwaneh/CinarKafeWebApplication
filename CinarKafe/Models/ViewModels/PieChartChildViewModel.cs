using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CinarKafe.Models.ViewModels
{
    public class PieChartChildViewModel
    {
        public List<string> backgroundColor { get; set; }
        public List<int> data { get; set; }

        public PieChartChildViewModel()
        {
            backgroundColor = new List<string>();

            data = new List<int>();
        }
    }
}