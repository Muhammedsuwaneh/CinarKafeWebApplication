using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;


namespace CinarKafe.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        [Required(ErrorMessage = "Kullanici Adi gereklidir")]
        [StringLength(255)]
        [Display(Name = "Ad")]
        public string KullanicAdi { get; set; }

        [Required(ErrorMessage = "Telefon Numara gereklidir")]
        [StringLength(255)]
        [Display(Name = "Telefon Numara")]
        public string TelefonNumara { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        /// <summary>
        /// Garsonlar 
        /// </summary>
        public DbSet<Garson> Garsons { get; set; }

        /// <summary>
        /// Masalar
        /// </summary>
        public DbSet<Masa> Masalar { get; set; }

        /// <summary>
        /// Masa durumlar
        /// </summary>
        public DbSet<MasaDurumu> MasaDurumlar { get; set; }

        /// <summary>
        /// Urun modeli
        /// </summary>
        public DbSet<Urun> Uruns { get; set; }

        /// <summary>
        /// Ürün kategoriler 
        /// </summary>
        public DbSet<UrunKategorisi> UrunKategorisis { get; set; }

        /// <summary>
        /// Stok modeli 
        /// </summary>
        public DbSet<Stok> Stoks { get; set; }

        public DbSet<Siperis> Siperis { get; set; }

        public DbSet<Siperisler> Siperisler { get; set; }

        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}