using AgriMarket.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AgriMarket.Data
{
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext>options):base(options){ }
        public DbSet<BestSeller> BestSeller { get; set; }
        public DbSet<ContactIUS> contactIUs { get; set; }
        public DbSet<FeedBack> feedBacks { get; set; }
        public DbSet<Product> products { get; set; }
        public DbSet<Slider> sliders { get; set; }
        
        
    }
}
