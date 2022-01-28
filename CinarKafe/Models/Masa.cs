using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CinarKafe.Models
{
    public class Masa
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Masa Numarasi gereklidir")]
        [Display(Name = "Masa Numarasi")]
        public int MasaNumarasi { get; set; }

        [Required(ErrorMessage = "Masa Kapasitesi gereklidir")]
        [Display(Name = "Masa Kapasitesi")]
        public int MasaKapasitesi { get; set; }

        public int KacKisiOturuyor { get; set; }

        public MasaDurumu MasaDurumu { get; set; }

        [Display(Name = "Masa Durumu")]
        public int MasaDurumuId { get; set; } 
    }
}