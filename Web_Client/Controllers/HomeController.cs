using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Security.Policy;
using System.Xml.Linq;
using Web_Client.Models;


namespace Web_Client.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        public static List<Dish> shoppingCart = new List<Dish>();


        public IActionResult Index()
        {
            return View();
        }

        async public Task<List<Dish>> MenuTest()
        {
            var url = "http://localhost:8080/api/menu";
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var MenuRespone = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<Dish>>(MenuRespone);
                }
                else
                {
                    return new List<Dish>();
                }
            }
        }

        //корзина покупок
        public List<Dish> Bag()
        {
            return shoppingCart.ToList();
        }

        [HttpPost("/Home/Menu")]
        public IActionResult Menu(int x)
        {
            shoppingCart.Add(ViewBag.Dish);
            return View();
        }

        [HttpGet]
        public IActionResult Menu()
        {
            var MenuRespone = MenuTest();
            var DishList = MenuRespone.Result;
            ViewBag.DishList = DishList;
            ViewBag.shoppingCart = shoppingCart;
            ViewBag.Dish = new Dish();
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
