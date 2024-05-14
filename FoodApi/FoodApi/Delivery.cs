using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com
{
	[Table("delivery")]
	public class Delivery
	{
		[Column("id")]
		public int Id { get; set; }

		[Column("order")]
		public string Order { get; set; }	

	}
}
