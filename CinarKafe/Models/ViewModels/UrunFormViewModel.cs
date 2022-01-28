using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;
using System.Web.Mvc;

namespace CinarKafe.Models.ViewModels
{
    public class UrunFormViewModel
    {
        public IEnumerable<UrunKategorisi> UrunKategorisi { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Urun Adi gereklidir")]
        [StringLength(255)]
        [Display(Name = "Ürün Adı")]
        public string UrunAdi { get; set; }

        [Required(ErrorMessage = "Fiyat gereklidir")]
        public double Fiyat { get; set; }

        [Required(ErrorMessage = "Stok Miktari gereklidir")]
        [Display(Name = "Stok Miktari")]
        public int StokMiktari { get; set; }

        [Display(Name = "Ürün Kategorisi")]
        public int UrunKategorisiId { get; set; }

        [Display(Name = "Resim")]
        public string Resim { get; set; }


        public UrunFormViewModel()
        {
            Id = 0;
        }

        public UrunFormViewModel(Urun urun)
        {
            Id = urun.Id;
            UrunAdi = urun.UrunAdi;
            Fiyat = urun.Fiyat;
            StokMiktari = urun.StokMiktari;
            UrunKategorisiId = urun.UrunKategorisiId;
            Resim = urun.Resim;
        }
    }
}