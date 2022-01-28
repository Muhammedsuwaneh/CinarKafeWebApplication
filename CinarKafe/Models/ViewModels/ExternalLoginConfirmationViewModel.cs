using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CinarKafe.Models.ViewModels
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required(ErrorMessage = "Kullanici adini giriniz")]
        [Display(Name = "Kullanici Adi")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Telefon Numara gereklidir")]
        [StringLength(255)]
        [Display(Name = "Telefon Numara")]
        public string TelefonNumara { get; set; }
    }
}