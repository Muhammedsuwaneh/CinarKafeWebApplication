using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CinarKafe.Models
{
    public class UrunKategorisi
    {
        public int Id { get; set; }
       
        [StringLength(255)]
        public string KategoriAdi { get; set; } 
    }
}