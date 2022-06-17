using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Net;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using WebApi.Entities;

namespace WebApi.Controllers
{
    public class SignInController : Controller
    {
        private IConfiguration _config;

        public SignInController(IConfiguration config)
        {
            _config = config;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SignIn(User login)
        {
            if (login.Username == "admin" && login.Password == "admin12345")
            {
                login = new User { Username = "admin", EmailAddress = "admin@gmail.com", PhoneNumber="0555555555", Role="Admin" };

                var claims = new[]
                {
                    new Claim(ClaimTypes.Name, login.Username),
                    new Claim(ClaimTypes.Email, login.EmailAddress),
                    new Claim(ClaimTypes.MobilePhone, login.PhoneNumber),
                    new Claim(ClaimTypes.Role, login.Role)
                };

                var identity = new ClaimsIdentity(
                    claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var principal = new ClaimsPrincipal(identity);

                var props = new AuthenticationProperties();

                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, props).Wait();

                return LocalRedirect("/Home/Index");
            }
            else
            {
                TempData["Error"] = "Incorrect Username or Password";
                return RedirectToAction("Index");
            }
        }

        public async Task<IActionResult> SignOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return LocalRedirect("/Home/Index");
        }
    }
}
