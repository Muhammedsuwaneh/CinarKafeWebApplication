using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CinarKafe.Models
{
    public class MasaDurumu
    {
        public int Id { get; set; }

        [StringLength(255)]
        public string DurumAdi { get; set; }
    }
}