using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CinarKafe.Models.ViewModels
{
    public class BarChartChildViewModel
    {
        public List<string> backgroundColor { get; set; }

        public List<string> borderColor { get; set; }

        public List<int> data { get; set; }

        public double borderWidth { get; set; }

        public string hoverBorderColor { get; set; }

        public string label = "Çınar Kafe";


        public BarChartChildViewModel()
        {
            backgroundColor = new List<string>();

            data = new List<int>();
        }
    }
}