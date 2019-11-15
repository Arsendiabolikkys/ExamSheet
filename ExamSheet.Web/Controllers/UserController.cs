using ExamSheet.Business.Account;
using ExamSheet.Web.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ExamSheet.Web.Controllers
{
    public class UserController : Controller
    {
        protected AccountManager AccountManager { get; set; }

        public UserController(AccountManager accountManager)
        {
            AccountManager = accountManager;
        }

        [HttpGet]
        public IActionResult Login()
        {
            var model = new LoginViewModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async  Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            if (AccountManager.IsPasswordValid(model.Email, model.Password))
            {
                var account = AccountManager.GetByEmail(model.Email);
                await Authenticate(account);
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError(string.Empty, "Невірний е-mail або пароль.");
            return View(model);
        }

        private Task Authenticate(AccountModel account)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, account.Email),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, account.AccountType.ToString()),
                new Claim(Constants.Claims.ReferenceId, account.ReferenceId)
            };
            var id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            return HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
    }
}