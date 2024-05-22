using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text;
using Web_Client.Models;

namespace Web_Client.Controllers
{
    public class AdminController : Controller
    {
        public static List<Dish> Dishes = new List<Dish>();
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult NewDish()
        {
            return View();
        }
        public IActionResult Menu()
        {
            var MenuRespone = MenuResponse();
            var DishList = MenuRespone.Result;
            ViewBag.DishList = DishList;
            Dishes = DishList;
            return View();
        }

        async public Task<List<Dish>> MenuResponse()
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
        // DELETE api/menu/5
        //[HttpDelete("{id}")]
        [HttpPost("/Admin/Delete")]
        async public Task<IActionResult> MenuDelete(List<int> selectedDishes)
        {
            foreach (var dishId in selectedDishes)
            {
                var url = $"http://localhost:8080/api/menu/{dishId}";
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.DeleteAsync(url);
                }
            }
            return RedirectToAction("Menu", "Admin");
        }

        [HttpPost("/Admin/MenuAdd")]
        async public Task<ActionResult> MenuAdd(string name, int price)
        {
            var d = new Dish {Name = name, Price = price };
            var url = "http://localhost:8080/api/menu";
            using (HttpClient client = new HttpClient())
            {
                string json = JsonConvert.SerializeObject(d);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(url, content);
            }
            return RedirectToAction("Menu", "Admin");
        }






        async public Task<List<Users>> UsersTest()
        {
            var url = "http://localhost:8081/api/users";
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var UsersRespone = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<Users>>(UsersRespone);
                }
                else
                {
                    return new List<Users>();
                }
            }
        }


        public IActionResult Users()
        {
            var UsersList = UsersTest().Result;
            ViewBag.Users = UsersList;
            return View();
        }

    }
}
    
