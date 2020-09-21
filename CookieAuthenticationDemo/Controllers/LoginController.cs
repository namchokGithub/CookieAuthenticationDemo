using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CookieAuthenticationDemo.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace CookieAuthenticationDemo.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult UserLogin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UserLogin([Bind] Users user)
        {
            // username = anet
            var users = new Users();
            var allUsers = users.GetUsers().FirstOrDefault();
            if (users.GetUsers().Any(u => u.UserName == user.UserName))
            {
                var userClaims = new List<Claim>()
                   {
                   new Claim(ClaimTypes.Name, user.UserName),
                   new Claim(ClaimTypes.Email, "anet@test.com"),
                    };

                var grandmaIdentity =
                    new ClaimsIdentity(userClaims, "User Identity");

                var userPrincipal = new ClaimsPrincipal(new[] { grandmaIdentity });
                HttpContext.SignInAsync(userPrincipal);

                return RedirectToAction("Index", "Home");
            }

            return View(user);
        }
    }
}
