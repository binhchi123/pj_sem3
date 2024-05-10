using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sem3.Areas.Admin.Models.BusinessModels;
using sem3.Areas.Admin.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sem3.Controllers
{
    public class WalletController : Controller
    {
        private readonly OMRContext _context;
        public WalletController(OMRContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(int Amount)
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId.HasValue)
            {
                var wallet = _context.Wallets.FirstOrDefault(w => w.UserId == userId.Value);
                if (wallet != null)
                {
                    wallet.Amount += Amount;
                    ViewBag.success = "<div class='alert alert-success'>Recharged successfully.</div>";
                }
                else
                {
                    wallet = new WalletModel
                    {
                        UserId = userId.Value,
                        Amount = Amount
                    };
                    _context.Wallets.Add(wallet);
                }
                _context.SaveChanges();
            }
            else
            {
                ViewBag.error = "<div class='alert alert-success'>Recharged failed.</div>";
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
