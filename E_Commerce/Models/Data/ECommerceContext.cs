using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Models.Data
{
    public class ECommerceContext:DbContext
    {
        public ECommerceContext(DbContextOptions options):base(options)
        { 
        }
        public DbSet<Category> Categories { set; get; }
        public DbSet<Customer> Customers { set; get; }
        public DbSet<Image> Images { set; get; }
        public DbSet<Item> Items { set; get; }
        public DbSet<Order> Orders { set; get; }
        public DbSet<Product> Products { set; get; }
        
        
    }
}