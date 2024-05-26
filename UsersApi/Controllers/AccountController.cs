using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UsersApi.Models;

namespace UsersApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost]
        [Route("register")]
        public async Task<User> Register(ProUser u)
        {
            User user = new User { Email = u.Email, UserName = u.Email, Name = u.Name, Address = u.Address, Phone = u.Phone };
            // добавляем пользователя
            var result = await _userManager.CreateAsync(user, u.Password);
            if (result.Succeeded)
            {
                // установка куки
                await _signInManager.SignInAsync(user, false);
                return user;
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return new User { };
            }
        }

        [HttpPost]
        [Route("login")]
        public async Task<string> Login(ProUser model)
        {
            var result =
                await _signInManager.PasswordSignInAsync(model.Email, model.Password, true, false);
            if (result.Succeeded)
            {
                return $"{model.Email} Успешно вошел!";
            }
            else
            {
                return "ERROR";
            }
        }
        [HttpGet]
        [Route("check")]
        public string Check()
        {
            var userId = _signInManager.UserManager.GetUserId(User);
            if (userId == null)
            {
                return "NOT";
            }
            return "YYYYEEEESS";
        }

        [HttpPost]
        [Route("logout")]
        public async Task<string> Logout()
        {
            //// удаляем аутентификационные куки
            await _signInManager.SignOutAsync();
            return "Успешно вышел!";
        }
    }
}
