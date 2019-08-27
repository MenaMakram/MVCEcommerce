using System.Collections.Generic;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace MVCEcommerce.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {

            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
        public virtual ICollection<Orders> Orders { get; set; } = new List<Orders>();
        //public virtual ICollection<ApplicationRole> ApplicationRoles { get; set; }
        
    }
    public class ApplicationRole : IdentityRole
    {
        //public virtual ICollection<ApplicationUser> ApplicationUsers { get; set; }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base(@"Data Source=.\SQLEXPRESS;Initial Catalog=ERBDatabase35;Integrated Security=True")
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
        public virtual DbSet<Category> Categories { get; set; }
        
        public virtual DbSet<Product> Products { get; set; }
        
        public virtual DbSet<OrderUserProduct> OrderUserProducts { get; set; }
        public virtual DbSet<Orders> Orders { get; set; }
        public virtual DbSet<ApplicationRole> ApplicationRoles { get; set; }
       // public virtual DbSet<ApplicationUser> ApplicationUsers { get; set; }

    }
}