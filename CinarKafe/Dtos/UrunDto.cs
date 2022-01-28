using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CinarKafe.Models;
using System.Web.Mvc;

namespace CinarKafe.Dtos
{
    public class UrunDto
    {
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

        public UrunKategorisi UrunKategorisi { get; set; }

        [Display(Name = "Ürün Kategorisi")]
        public int UrunKategorisiId { get; set; }


        [Display(Name = "Resim")]
        public string Resim { get; set; }
    }
}