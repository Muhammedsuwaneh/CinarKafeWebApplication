using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;
using CinarKafe.Models;

namespace CinarKafe.Dtos
{
    public class MasaDto
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

        public MasaDurumu MasaDurumu { get; set; }

        [Display(Name = "Masa Durumu")]
        public int MasaDurumuId { get; set; }
    }
}