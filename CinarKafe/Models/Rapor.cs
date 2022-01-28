using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CinarKafe.Models
{
    public class Rapor
    {
        public byte Id { get; set; }

        public Urun Urun { get; set; }

        public byte UrunId { get; set; }

        [Display(Name = "Kaç tane satıldı")]
        public int KacTaneSatildi { get; set; }

        [Display(Name = "Günlük Geliri")]
        public double GunlukGeliri { get; set; }

        public DateTime Tarihi { get; set; }
    }
}