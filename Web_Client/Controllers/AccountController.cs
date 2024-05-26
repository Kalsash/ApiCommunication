using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using Web_Client.Models;
namespace Web_Client.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        [Route("account/test")]
        public string Test()
        {
            return "ok";
        }

        [HttpPost]
        [Route("account/register")]
        async public Task<IActionResult> Register()
        {
            var user = new Users { Name = "Oscar Montenegro", Phone = "+8 800 555 35 35", Address = "Moscow,Pushkinskaya 89" };
            var url = "http://localhost:8081/account/register";
            using (HttpClient client = new HttpClient())
            {
                string json = JsonConvert.SerializeObject(user);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(url, content);
            }
            return View();
        }
        //async public Task<ActionResult> MenuAdd(string name, int price)
        //{
        //    var d = new Dish { Name = name, Price = price };
        //    var url = "http://localhost:8080/api/menu";
        //    using (HttpClient client = new HttpClient())
        //    {
        //        string json = JsonConvert.SerializeObject(d);
        //        StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
        //        HttpResponseMessage response = await client.PostAsync(url, content);
        //    }
        //    return RedirectToAction("Menu", "Admin");
        //}
    }
}
