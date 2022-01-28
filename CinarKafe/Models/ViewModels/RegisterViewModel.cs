using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CinarKafe.Models.ViewModels
{
    public class RegisterViewModel
    {

        [Required]
        [EmailAddress]
        [Display(Name = "Eposta")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "{0} en az {2} karakter uzunluğunda olmalıdır", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Şifre")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Şifre Onayla")]
        [Compare("Password", ErrorMessage = "Şifreler aynı değil")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Kullanici Adi gereklidir")]
        [StringLength(255)]
        [Display(Name = "Kullanici Adi")]
        public string KullanicAdi { get; set; }


        [Required(ErrorMessage = "Telefon Numara gereklidir")]
        [StringLength(255)]
        [Display(Name = "Telefon Numara")]
        public string TelefonNumara { get; set; }
    }
}