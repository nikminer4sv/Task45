using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Task45.ViewModels;
using Task45.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace Task45.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpGet]
        [Authorize]
        public IActionResult Profile()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = new User { Email = model.Email, UserName = model.Email, Name = model.Name, Surname = model.Surname, RegisterDate = DateTime.Now, LastLoginDate = DateTime.Now, Status = "ok" };
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
                        ModelState.AddModelError(string.Empty, error.Description);
                    
                }
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Login(string? returnUrl = null)
        {
            return View(new LoginViewModel { ReturnUrl = returnUrl });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                User? user = _userManager.Users.FirstOrDefault(u => u.Email == model.Email);
                if (user != null)
                    if (user.Status != "ok")
                        return View(model);

                var result =
                    await _signInManager.PasswordSignInAsync(model.Email, model.Password, true, false);

                if (result.Succeeded)
                {
                    user.LastLoginDate = DateTime.Now;
                    await _userManager.UpdateAsync(user);
                    // проверяем, принадлежит ли URL приложению
                    if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Неправильный логин и (или) пароль");
                }
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            // удаляем аутентификационные куки
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> DeleteUsers(string[] ids)
        {
            if (UserCheck())
            {
                Logout();
                return RedirectToAction("Index", "Home");
            }

            foreach (string id in ids)
            {
                User? user = _userManager.Users.FirstOrDefault(u => u.Id == id);
                if (user != null)
                {
                    await _userManager.DeleteAsync(user);
                    if (user.Email == User.Identity.Name)
                        await Logout();
                    
                }
            }

            return RedirectToAction("Index", "Home");

        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> BlockUsers(string[] ids)
        {
            if (UserCheck())
            {
                Logout();
                return RedirectToAction("Index", "Home");
            }

            foreach (string id in ids)
            {
                User? user = _userManager.Users.FirstOrDefault(u => u.Id == id);
                if (user != null)
                {
                    user.Status = "blocked";
                    await _userManager.UpdateAsync(user);
                    if (user.Email == User.Identity.Name)
                        await Logout();

                }
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> UnblockUsers(string[] ids)
        {

            if (UserCheck())
            {
                Logout();
                return RedirectToAction("Index", "Home");
            }

            foreach (string id in ids)
            {
                User? user = _userManager.Users.FirstOrDefault(u => u.Id == id);
                if (user != null)
                {
                    user.Status = "ok";
                    await _userManager.UpdateAsync(user);
                }
            }

            return RedirectToAction("Index", "Home");
        }

        public bool UserCheck()
        {
            User? user = _userManager.Users.FirstOrDefault(u => u.Email == User.Identity.Name);
            if (user == null || user.Status != "ok")
                return true;
            

            return false;
        }

    }
}