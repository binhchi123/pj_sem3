using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using sem3.Areas.Admin.Models.BusinessModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sem3.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    [Route("Admin")]
    public class HomeController : Controller
    {
        private OMRContext _context;
        public HomeController(OMRContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
