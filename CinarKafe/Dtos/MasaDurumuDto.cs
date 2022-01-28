using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace CinarKafe.Dtos
{
    public class MasaDurumuDto
    {
        public int Id { get; set; }

        [StringLength(255)]
        public string DurumAdi { get; set; }
    }
}