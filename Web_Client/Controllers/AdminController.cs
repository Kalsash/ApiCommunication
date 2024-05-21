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
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult NewDish()
        {
            return View();
        }


        async public Task<string> Menu()
        {
            var url = "http://localhost:8080/api/menu";
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var MenuRespone = await response.Content.ReadAsStringAsync();
                    return MenuRespone;
                }
                else
                {
                    return $"Error: {response.StatusCode}";
                }
            }
        }
        [HttpPost("/Admin/Test")]
        async public Task<ActionResult> Test(string name, int price)
        {
            var d = new Dish {Name = name, Price = price };
            var url = "http://localhost:8080/api/menu";
            using (HttpClient client = new HttpClient())
            {
                string json = JsonConvert.SerializeObject(d);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(url, content);
            }
            return RedirectToAction("Menu", "Home");
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
    
