using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using CinarKafe.Models;
using CinarKafe.Dtos;

namespace CinarKafe.Models.ViewModels
{
    /// <summary>
    /// Masa view model - temporal storage and display of masa info
    /// </summary>
    public class MasaFormViewModel
    {
        public IEnumerable<MasaDurumu> MasaDurumu { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Masa Numarasi gereklidir")]
        [Display(Name = "Masa Numarasi")]
        public int MasaNumarasi { get; set; }

        [Required(ErrorMessage = "Masa Kapasitesi gereklidir")]
        [Display(Name = "Masa Kapasitesi")]
        public int MasaKapasitesi { get; set; }

        [Display(Name = "Masa Durumu")]
        public int MasaDurumuId { get; set; }

        /// <summary>
        /// Default constructor 
        /// </summary>
        public MasaFormViewModel()
        {
            Id = 0;
            MasaDurumuId = 1; // available - boş
        }

        /// <summary>
        /// Init masa 
        /// </summary>
        /// <param name="masa"></param>
        public MasaFormViewModel(Masa masa)
        {
            Id = masa.Id;
            MasaNumarasi = masa.MasaNumarasi;
            MasaKapasitesi = masa.MasaKapasitesi;
            MasaDurumuId = 1; // new masa will be available for use 
        }
    }
}