using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using MenuApi.Models;

namespace MenuApiData
{
	public class MenuContext : DbContext
	{
        public MenuContext(DbContextOptions<MenuContext> options) : base(options)
		{
			Database.EnsureCreated();
		}
		protected override void OnModelCreating(ModelBuilder builder)
		{
			builder.Entity<Menu>().HasData(
			  new Menu { Id = 1, Name = "Meat", Price = 500 },
			  new Menu { Id = 2, Name = "Fish", Price = 400 }
			);
		}
		public DbSet<Menu> MenuList { get; set; }
	}
}
