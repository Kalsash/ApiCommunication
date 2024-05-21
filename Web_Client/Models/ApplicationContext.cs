using Microsoft.EntityFrameworkCore;

namespace Web_Client.Models
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Dish> ShoppingCart { get; set; } = null!;

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options)
        {
            Database.EnsureCreated();   // создаем базу данных при первом обращении
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Dish>().HasData(
                    new Dish { Id = 1, Name = "Crab", Price = 2200 },
                    new Dish { Id = 2, Name = "Octopus", Price = 1500 }
                    );               
        }
    }

}
