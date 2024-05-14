using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com
{
	public class Order
	{
		public void AddDish(string name, decimal price)
		{
			DishList.Add(name);
			TotalPrice += price;
		}
        public List<string> DishList = new List<string>();

		public decimal TotalPrice = 0;
       
	}
}
