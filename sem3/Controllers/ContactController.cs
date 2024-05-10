using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using sem3.Areas.Admin.Models.BusinessModels;
using sem3.Areas.Admin.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sem3.Controllers
{
    public class ContactController : Controller
    {
        private readonly OMRContext _context;
        public ContactController(OMRContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(ContactModel model)
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId.HasValue)
            {
                model.UserId = userId.Value;
                model.DateOfMessage = DateTime.Now;
                _context.Contacts.Add(model);
                _context.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
    }
}
