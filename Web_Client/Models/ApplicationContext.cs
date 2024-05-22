using Microsoft.EntityFrameworkCore;

namespace Web_Client.Models
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Cart> ShoppingCart { get; set; } = null!;

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options)
        {
            Database.EnsureCreated();   // создаем базу данных при первом обращении
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cart>().HasData(
                    new Cart { Id = 1, Id_u = 1, Id_m = "1;3"});               
        }
    }

}
