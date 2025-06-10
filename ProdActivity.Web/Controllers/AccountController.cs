using Microsoft.AspNetCore.Mvc;
using ProdActivity.DataAccess;
using ProdActivity.DataAccess.Repositories;
using ProdActivity.Entities;
using ProdActivity.Web.Models;

namespace ProdActivity.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserRepository _userRepository;

        public AccountController()
        {
            _userRepository = new UserRepository(new AppDbContext());
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                User? user = _userRepository.Login(model.Username, model.Password);

                if (user != null)
                {
                    Models.CurrentUser.User = user; 
                    return RedirectToAction("Index", "Dashboard");
                }
                else
                {
                    ModelState.AddModelError("", "Kullanıcı adı veya şifre hatalı.");
                }
            }
            return View(model);
        }

        public IActionResult Logout()
        {
            Models.CurrentUser.User = null;
            return RedirectToAction("Login");
        }
    }
}
