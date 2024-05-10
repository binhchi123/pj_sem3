using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using sem3.Areas.Admin.Models;
using sem3.Areas.Admin.Models.BusinessModels;
using sem3.Areas.Admin.Models.DataModels;
using sem3.Areas.Admin.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Utility = sem3.Areas.Admin.Models.Utility;

namespace sem3.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AuthController : Controller
    {
        private readonly OMRContext _context;
        public AuthController(OMRContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Signin(string urlRedirect)
        {
            ViewBag.urlRedirect = urlRedirect;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Signin(string email, string password, string urlRedirect)
        {
            if (string.IsNullOrEmpty(email))
            {
                ViewBag.error = "<div class='alert alert-danger'>You have entered an incorrect email address.</div>";
                return View();
            }
            if (string.IsNullOrEmpty(password))
            {
                ViewBag.error = "<div class='alert alert-danger'>You have entered an incorrect password.</div>";
                return View();
            }   
            if (ModelState.IsValid)
            {
                var md5pass = Utility.MD5Hash(password);
                var accFound = await _context.Users.Include(r => r.Role).FirstOrDefaultAsync(x => x.Email == email && x.Password == md5pass);
                if (accFound != null)
                {
                    if (accFound.RoleId == 1)
                    {
                        var identity = new ClaimsIdentity(new[]
                       {
                            new Claim("UserId", accFound.UserId.ToString()),
                            new Claim(ClaimTypes.Name, accFound.UserName),
                            new Claim(ClaimTypes.Role, accFound.Role.RoleName),
                            
                        }, CookieAuthenticationDefaults.AuthenticationScheme);
                        var principal = new ClaimsPrincipal(identity);
                        var login =  HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                        if (!string.IsNullOrEmpty(urlRedirect))
                        {
                            return Redirect(urlRedirect);
                        }
                        return Redirect("/Admin");
                    }
                    ViewBag.error = "<div class='alert alert-danger'>Login failed</div>";
                    return View("Signin");
                }                 
            }
            ViewBag.error = "<div class='alert alert-danger'>Login failed</div>";
            return View("Signin");
        }

        [HttpGet]
        public IActionResult Signout()
        {
            var login = HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            if (login.IsCompletedSuccessfully)
            {
                return RedirectToAction("Signin", "Auth");
            }
            return Redirect("/Admin/Home/Index");
        }
    }
}

