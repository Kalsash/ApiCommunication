﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com
{
	public class User
	{
        public string? Name { get; set; }

		public string? Phone { get; set; }

		public string Address { get; set; }
	}
}
