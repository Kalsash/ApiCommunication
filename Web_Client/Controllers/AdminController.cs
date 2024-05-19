using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using Web_Client.Models;

namespace Web_Client.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
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
        //post
        async public Task<string> AddNewDish(Dish d)
        {
            var url = "http://localhost:8080/api/menu";
            using (HttpClient client = new HttpClient())
            {
                //var d = new Dish({Name = "bebra", Price = 1123});
                string json = JsonConvert.SerializeObject(d);
                Console.WriteLine(json);

                // Создайте экземпляр HttpContent, если вам нужно отправить данные в теле запроса.
                // Например, для JSON:
                // string json = "{\"key\":\"value\"}";
                // StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                StringContent content = new StringContent(string.Empty); // Пустое тело запроса

                HttpResponseMessage response = await client.PostAsync(url, content);

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
    
