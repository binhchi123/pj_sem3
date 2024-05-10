using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using sem3.Areas.Admin.Models.BusinessModels;
using sem3.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace sem3.Controllers
{
    public class HomeController : Controller
    {
        private OMRContext _context;
        public HomeController(OMRContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index(int? ServiceProviderId)
        {
            var userId = HttpContext.Session.GetInt32("UserID");
            var userFound = await _context.Users.FirstOrDefaultAsync(x => x.UserId == userId);
            ViewBag.userFound = null;
            if (userFound != null)
            {
                ViewBag.userFound = userFound;
                Console.WriteLine($"UserID Header: {userFound?.UserId}");
            }
            var data = _context.RechargePlans.ToList();
            ViewBag.ServiceProviderId = _context.ServiceProviders.ToList();
            if (ServiceProviderId != null)
            {
                data = _context.RechargePlans.Where(s => s.ServiceProviderId == ServiceProviderId).ToList();
            }
            return View(data);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.RechargePlans == null)
            {
                return NotFound();
            }

            var rechargePlansModel = await _context.RechargePlans
                .Include(r => r.ServiceProvider)
                .FirstOrDefaultAsync(m => m.RechargePlanId == id);
            if (rechargePlansModel == null)
            {
                return NotFound();
            }

            return View(rechargePlansModel);
        }

        public IActionResult Bill()
        {
            return View();
        }
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Help()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
