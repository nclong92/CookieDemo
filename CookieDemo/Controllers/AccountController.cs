using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CookieDemo.Controllers
{
    public class AccountController : Controller
    {

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string userName, string password)
        {
            if (!string.IsNullOrEmpty(userName) && string.IsNullOrEmpty(password))
            {
                return RedirectToAction("Login");
            }

            // check user name and password
            // Here can be implemented checking logic from the database
            ClaimsIdentity identity = null;
            bool isAuthenticated = false;

            if (userName == "Admin" && password == "password")
            {
                // Create the identity for the user
                identity = new ClaimsIdentity(new[]
                            {
                                new Claim(ClaimTypes.Name, userName),
                                new Claim(ClaimTypes.Role, "Admin")
                            }, CookieAuthenticationDefaults.AuthenticationScheme);

                isAuthenticated = true;
            }

            if (userName == "Guest" && password == "password")
            {
                // Create the identity for the user
                identity = new ClaimsIdentity(new[]
                            {
                                new Claim(ClaimTypes.Name, userName),
                                new Claim(ClaimTypes.Role, "Guest")
                            }, CookieAuthenticationDefaults.AuthenticationScheme);

                isAuthenticated = true;
            }

            if (isAuthenticated)
            {
                var principal = new ClaimsPrincipal(identity);

                var login = HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                return RedirectToAction("Index", "Home");
            }

            return View();
        }


        //[HttpPost]
        public IActionResult Logout()
        {
            var login = HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }

    }
}
