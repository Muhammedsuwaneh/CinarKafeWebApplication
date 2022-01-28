using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace CinarKafe.Dtos
{
    public class GarsonDto
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }


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

    }
}