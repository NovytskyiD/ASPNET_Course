using Microsoft.EntityFrameworkCore;
using OnlineShop.Db.Models;

namespace OnlineShop.Db
{
    public class DatabaseContext:DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Favorites> Favorites { get; set; }
        public DbSet<ProductCompare> ProductCompares { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DatabaseContext (DbContextOptions<DatabaseContext> options):base (options)
        {
            Database.Migrate();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasData(new Product { Id=1, Cost=1000, Name="Продукт1", ImagePath="/img/1.jpg",Descriprion="Описание1" },
                new Product { Id = 2, Cost = 2000, Name = "Продукт2", ImagePath = "/img/2.jpg", Descriprion = "Описание2" },
                new Product { Id = 3, Cost = 3000, Name = "Продукт3", ImagePath = "/img/3.jpg", Descriprion = "Описание3" });
        }

    }
}
