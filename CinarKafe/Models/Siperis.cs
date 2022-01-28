using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CinarKafe.Models
{
    public class Siperis
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("UrunId")]
        public Urun Urun { get; set; }

        public int UrunId { get; set; }

        public int Adet { get; set; }

        public double ToplamFiyat { get; set; }

        public DateTime Tarihi { get; set; }
    }
}