using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using CinarKafe.Dtos;
using System.ComponentModel.DataAnnotations.Schema;

namespace CinarKafe.Models.ViewModels
{
    public class GarsonFormViewModel
    {
        public int? Id { get; set; }


        [Required(ErrorMessage = "Garson Adi gereklidir")]
        [StringLength(255)]
        public string Ad { get; set; }


        [Required(ErrorMessage = "Garson Soyadi gereklidir")]
        [StringLength(255)]
        public string Soyad { get; set; }


        [Required(ErrorMessage = "Garson Soyadi gereklidir")]
        [StringLength(255)]
        public string SCN { get; set; }

        [Required(ErrorMessage = "Eposta gereklidir")]
        [StringLength(255)]
        public string Eposta { get; set; }


        [Required(ErrorMessage = "Adres gereklidir")]
        [StringLength(255)]
        public string Adres { get; set; }


        [Required(ErrorMessage = "Telefon Numara gereklidir")]
        [StringLength(255)]
        [Display(Name = "Tefelon Numara")]
        public string TelefonNumara { get; set; }


        [Required(ErrorMessage = "Maaş gereklidir")]
        [Display(Name = "Maaş")]
        public double Maas { get; set; }

        public GarsonFormViewModel()
        {
            Id = 0;
        }

        public GarsonFormViewModel(Garson garson)
        {
            Id = garson.Id;
            Ad = garson.Ad;
            Soyad = garson.Soyad;
            SCN = garson.SCN;
            Eposta = garson.Eposta;
            Adres = garson.Adres;
            TelefonNumara = garson.TelefonNumara;
            Maas = garson.Maas;
        }

        public string Title
        {
            get
            {
                return Id != 0 ? "Garson güncelle" : "Yeni Garson";
            }
        }
    }
}