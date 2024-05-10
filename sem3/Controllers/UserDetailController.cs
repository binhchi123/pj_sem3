using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sem3.Areas.Admin.Models.BusinessModels;
using sem3.Areas.Admin.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace sem3.Controllers
{
    public class UserDetailController : Controller
    {
        private readonly OMRContext _context;
        public readonly List<RechargeLogsModel> rechargeLogs = new List<RechargeLogsModel>();
        public UserDetailController(OMRContext context)
        {
            _context = context;
        }

        [Authorize]
        public ActionResult MiddleWareAction()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            var userName = HttpContext.Session.GetString("UserName");
            if (HttpContext.Session.GetInt32("userId") != null)
            {
                var userFound = _context.Users.FirstOrDefault(x => x.UserId == userId);
                ViewBag.userFound = null;
                if (userFound != null)
                {
                    ViewBag.userFound = userFound;
                    Console.WriteLine($"UserId Header: {userFound?.UserId}");
                }
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Account", new { returnUrl = !string.IsNullOrEmpty(HttpContext.Request.Path) ? HttpContext.Request.Path.ToString() : "" });
            }
        }

        // GET: UserDetails/Index/5
        [Authorize]
        public async Task<IActionResult> Index(int? id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }
            var usersModel = await _context.Users
                .Include(u => u.RechargePlans)
                .Include(u => u.ServiceProvider)
                .Include(w => w.Wallet)
                .FirstOrDefaultAsync(m => m.UserId == id);

            if (usersModel == null)
            {
                return NotFound();
            }
            ViewBag.userDetails = usersModel;


            if (id == null || _context.RechargeLogs == null)
            {
                return NotFound();
            }
            var rechargeLogs = await _context.RechargeLogs
             .Include(r => r.RechargePlans)
             .ThenInclude(rp => rp.ServiceProvider)
             .Include(r => r.Users)
             .Where(m => m.UserId == id)
             .ToListAsync();
            if (rechargeLogs == null)
            {
                return NotFound();
            }
            ViewBag.rechargeLogs = rechargeLogs;
            return View();
        }
    }
}
