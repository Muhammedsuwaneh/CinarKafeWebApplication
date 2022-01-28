using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CinarKafe.Models.ViewModels
{
    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Eposta")]
        public string Email { get; set; }
    }
}