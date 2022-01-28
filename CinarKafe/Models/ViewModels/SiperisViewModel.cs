using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CinarKafe.Models.ViewModels
{
    public class SiperisViewModel
    {
        // Urun

        public int UrunId { get; set; }

        public int UrunKategorisiId { get; set; }

        public string UrunAdi { get; set; }

        public string KategoriAdi { get; set; }

        public double Fiyat { get; set; }

        public int StokMiktari { get; set; }

        // siperis

        public int Adet { get; set; }

        public double ToplamFiyat { get; set; }

        public DateTime Tarihi { get; set; }

        public Masa Masa { get; set; }

        [Required(ErrorMessage = "Bir seçmeniz lazım")]
        public int MasaId { get; set; }

        public IEnumerable<Masa> Masalar { get; set; }

        public SiperisViewModel()
        {
        }
    }
}