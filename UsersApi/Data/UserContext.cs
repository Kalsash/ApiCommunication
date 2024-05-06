using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using UsersApi.Models;

namespace UsersApiData
{
	public class UserContext : DbContext
	{
        public UserContext(DbContextOptions<UserContext> options) : base(options)
		{
			Database.EnsureCreated();
		}
		protected override void OnModelCreating(ModelBuilder builder)
		{
			builder.Entity<User>().HasData(
			  new User { Id = 1, Name = "Oscar Montenegro", Phone = "+8 800 555 35 35", Address ="Moscow,Pushkinskaya 89" },
			  new User { Id = 2, Name = "Alex Griboedov", Address = "Omsk,Holodnaya 92" }
			);
		}
		public DbSet<User> Users { get; set; }
	}
}
