using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Web_Client.ViewModels;

namespace Web_Client.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<RegisterViewModel> _userManager;
        private readonly SignInManager<RegisterViewModel> _signInManager;

        public AccountController(UserManager<RegisterViewModel> userManager, SignInManager<RegisterViewModel> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                RegisterViewModel user = new RegisterViewModel { Email = model.Email, UserName = model.Email, Year = model.Year };

                // добавляем пользователя
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    // установка куки
                    await _signInManager.SignInAsync(user, false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(model);
        }
    }
}
