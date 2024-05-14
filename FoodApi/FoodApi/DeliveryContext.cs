using FoodApi.Controllers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com
{
	public class DeliveryContext : DbContext
	{
		public DbSet<Delivery> Deliveries { get; set; }
		public DeliveryContext(DbContextOptions<DeliveryContext> options) : base(options)
		{
			Database.EnsureCreated();
		}
		protected override void OnModelCreating(ModelBuilder builder)
		{
			//FoodController.DeliveryList.First();
			builder.Entity<Delivery>().HasData(
			  new Delivery { Id = 1, Order = "New order"}
			);
		}
	}
}
