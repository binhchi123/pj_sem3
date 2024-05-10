using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using sem3.Areas.Admin.Models;
using sem3.Areas.Admin.Models.BusinessModels;
using sem3.Areas.Admin.Models.DataModels;
using sem3.Areas.Admin.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace sem3.Controllers
{
    public class AccountController : Controller
    {
        private readonly OMRContext _context = new OMRContext();
        public AccountController(OMRContext context)
        {
            _context = context;
        }

        [HttpGet]
        //[Route("Login")]
        public IActionResult Login(string urlRedirect)
        {
            ViewBag.urlRedirect = urlRedirect;
            return View();
        }

        [HttpPost]
        //[Route("Login")]
        public async Task<IActionResult> Login([Bind("Phone, Password")] LoginViewModel account, string urlRedirect)
        {
            if (string.IsNullOrEmpty(account.Phone))
            {
                ViewBag.error = "<div class='alert alert-danger'>You have entered an incorrect phone.</div>";
                return View();
            }
            if (string.IsNullOrEmpty(account.Password))
            {
                ViewBag.error = "<div class='alert alert-danger'>You have entered an incorrect password.</div>";
                return View();
            }
            if (ModelState.IsValid)
            {
                var md5pass = Utility.MD5Hash(account.Password);
                var accFound = await _context.Users.FirstOrDefaultAsync(x => x.Phone == account.Phone && x.Password == md5pass);
                if (accFound != null)
                {
                    HttpContext.Session.SetString("IsLoggedIn", "true");
                    HttpContext.Session.SetInt32("UserId", accFound.UserId);
                    HttpContext.Session.SetString("UserName", accFound.UserName);
                    if (!string.IsNullOrEmpty(urlRedirect))
                    {
                        return Redirect(urlRedirect);
                    }
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.error = "<div class='alert alert-danger'>Login failed</div>";
                    return View(account);
                }
            }
            else
            {
                ViewBag.error = "<div class='alert alert-danger'>Login failed</div>";
                return View(account);
            }
        }

        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("IsLoggedIn");
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Register()
        {
            PopulateDropdownLists();
            return View();
        }

        [HttpPost]
        public IActionResult Register(UsersModel model)
        {
            if (ModelState.IsValid)
            {
                var existingUser = _context.Users.FirstOrDefault(u => u.Phone == model.Phone);
                if (existingUser != null)
                {
                    existingUser.RechargePlanId = model.RechargePlanId;
                    existingUser.ServiceProviderId = model.ServiceProviderId;
                    _context.SaveChanges();
                    ViewBag.success = "Register successfully";
                    return RedirectToAction("Login", "Account");
                }
                else
                {
                    string hashedPassword = Utility.MD5Hash(model.Password);
                    model.Password = hashedPassword;
                    model.RoleId = 2;
                    _context.SaveChanges(false);
                    _context.Users.Add(model);
                    _context.SaveChanges();
                    ViewBag.success = "Register successfully";
                    return RedirectToAction("Login", "Account");
                }
            }

            PopulateDropdownLists();
            ViewBag.error = "Register Failed!";
            return View(model);
        }

        private void PopulateDropdownLists()
        {
            ViewData["RechargePlanId"] = new SelectList(_context.RechargePlans, "RechargePlanId", "RechargePlanName");
            ViewData["ServiceProviderId"] = new SelectList(_context.ServiceProviders, "ServiceProviderId", "ServiceName");
        }
    }
}
