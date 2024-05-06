using System.ComponentModel.DataAnnotations.Schema;

namespace UsersApi.Models
{
	[Table("users")]
	public class User
	{
		[Column("id")]
		public int Id { get; set; }

		[Column("name")]
		public string? Name { get; set; }

		[Column("phone")]
		public string? Phone { get; set; }

		[Column("address")]
		public string Address { get; set; }
	}
}
