using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CinarKafe.Models
{
    public class Siperisler
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string ApplicationUserId { get; set; }
        [ForeignKey("ApplicationUserId")]
        public ApplicationUser ApplicationUser { get; set; }

        [ForeignKey("SiperisId")]
        public Siperis Siperis { get; set; }
        public int SiperisId { get; set; }

        [ForeignKey("MasaId")]
        public Masa Masa { get; set; }
        public int MasaId { get; set; }

        public bool odenmis { get; set; }
    }
}