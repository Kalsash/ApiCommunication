using System.ComponentModel.DataAnnotations.Schema;

namespace MenuApi.Models
{
	[Table("menu")]
	public class Menu
	{
		[Column("id")]
		public int Id { get; set; }

		[Column("name")]
		public string Name { get; set; }

		[Column("price")]
		public decimal Price { get; set; }
	}
}
