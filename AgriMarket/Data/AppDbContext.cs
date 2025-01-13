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
        public DbSet<Product> Products { get; set; }
       public DbSet<Farmer>? Farmers { get; set; }
       public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Slider> sliders { get; set; }
        public DbSet<OrderProduct> OrderProducts { get; set; }
        public DbSet<ProductItem>productItems { get; set; }
        
        
    }
}

