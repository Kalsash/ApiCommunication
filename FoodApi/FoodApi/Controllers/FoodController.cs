using Com;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace FoodApi.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class FoodController : Controller
	{
		//static string UsersRespone = "";
		//static string MenuRespone = "";

		public static List<string> DeliveryList = new List<string>();

		static string UsersRespone = "[\r\n  {\r\n    \"id\": 1,\r\n    \"name\": \"Oscar Montenegro\",\r\n    \"phone\": \"+8 800 555 35 35\",\r\n    \"address\": \"Moscow,Pushkinskaya 89\"\r\n  },\r\n  {\r\n    \"id\": 2,\r\n    \"name\": \"Alex Griboedov\",\r\n    \"phone\": null,\r\n    \"address\": \"Omsk,Holodnaya 92\"\r\n  }\r\n]";
		static string MenuRespone = "[\r\n  {\r\n    \"id\": 1,\r\n    \"name\": \"Meat\",\r\n    \"price\": 500\r\n  },\r\n  {\r\n    \"id\": 2,\r\n    \"name\": \"Fish\",\r\n    \"price\": 400\r\n  }\r\n]";

		static async Task GetData()
		{
			string url = "http://localhost:8080/api/users";

			using (HttpClient client = new HttpClient())
			{
				HttpResponseMessage response = await client.GetAsync(url);

				if (response.IsSuccessStatusCode)
				{
					UsersRespone = await response.Content.ReadAsStringAsync();
				}
				else
				{
					Console.WriteLine($"Error: {response.StatusCode}");
				}
			}
			url = "http://localhost:8081/api/menu";
			using (HttpClient client = new HttpClient())
			{
				HttpResponseMessage response = await client.GetAsync(url);

				if (response.IsSuccessStatusCode)
				{
					MenuRespone = await response.Content.ReadAsStringAsync();
				}
				else
				{
					Console.WriteLine($"Error: {response.StatusCode}");
				}
			}
		}

		static void GetOrder()
		{
			List<User> UsersList = JsonConvert.DeserializeObject<List<User>>(UsersRespone);
			List<Dish> Menu = JsonConvert.DeserializeObject<List<Dish>>(MenuRespone);
			HashSet<int> DishNums = new HashSet<int>();
			Dictionary<User, Order> dict = new Dictionary<User, Order>();
			foreach (var dish in Menu)
			{
				DishNums.Add(dish.Id);
			}
			foreach (var user in UsersList)
			{
				Random rand = new Random();
				int count = rand.Next(1, 6);
				var order = new Order();
				while (count > 0)
				{
					count--;
					int id = DishNums.ElementAt(rand.Next(DishNums.Count));
					foreach (var item in Menu)
					{
						if (item.Id == id)
						{
							order.AddDish(item.Name, item.Price);
							break;
						}
					}
				}
				dict.Add(user, order);
			}


			foreach (var keyValue in dict)
			{
				string ss = "";
				foreach (var dish in keyValue.Value.DishList)
				{
					ss += dish + ",";
				}
				var s = $"Name= {keyValue.Key.Name}; Phone= {keyValue.Key.Phone}; Address= {keyValue.Key.Address} => DishList= {ss} TotalPrice= {keyValue.Value.TotalPrice}";
				DeliveryList.Add(s);
			}
			foreach (var item in DeliveryList)
			{
				Console.WriteLine(item);
			}
			//using (DeliveryContext db = new DeliveryContext())
			//{
			//	//foreach (var ord in DeliveryList)
			//	//{
			//	//	Delivery delivery = new Delivery { Order = ord };
			//	//	db.Deliveries.Add(delivery);
			//	//}
			//	//db.SaveChanges();
			//}
		}

		[HttpGet]
		public List<string> Index()
		{
			//await GetData();
			GetOrder();
			return DeliveryList;
		}

		//private readonly DeliveryContext _context;

		//public FoodController(DeliveryContext context)
		//{
		//	_context = context;
		//}

		//// GET: api/users
		//[HttpGet]
		//public async Task<ActionResult<IEnumerable<Delivery>>> GetDeliveries()
		//{
		//	//GetOrder();
		//	return await _context.Deliveries.ToArrayAsync();
		//}
	}
}
