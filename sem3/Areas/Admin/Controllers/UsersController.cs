using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using sem3.Areas.Admin.Models.BusinessModels;
using sem3.Areas.Admin.Models.DataModels;

namespace sem3.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UsersController : Controller
    {
        private readonly OMRContext _context;

        public UsersController(OMRContext context)
        {
            _context = context;
        }

        // GET: Admin/Users
        public async Task<IActionResult> Index()
        {
            var oMRContext = _context.Users.Include(u => u.RechargePlans).Include(u => u.Role).Include(u => u.ServiceProvider);
            return View(await oMRContext.ToListAsync());
        }

        // GET: Admin/Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usersModel = await _context.Users
                .Include(u => u.RechargePlans)
                .Include(u => u.Role)
                .Include(u => u.ServiceProvider)
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (usersModel == null)
            {
                return NotFound();
            }

            return View(usersModel);
        }

        // GET: Admin/Users/Create
        public IActionResult Create()
        {
            ViewData["RechargePlanId"] = new SelectList(_context.RechargePlans, "RechargePlanId", "RechargePlanName");
            ViewData["RoleId"] = new SelectList(_context.Roles, "RoleId", "RoleId");
            ViewData["ServiceProviderId"] = new SelectList(_context.ServiceProviders, "ServiceProviderId", "ServiceName");
            return View();
        }

        // POST: Admin/Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,ServiceProviderId,RechargePlanId,UserName,Email,Phone,Password,RoleId")] UsersModel usersModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(usersModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RechargePlanId"] = new SelectList(_context.RechargePlans, "RechargePlanId", "RechargePlanName", usersModel.RechargePlanId);
            ViewData["RoleId"] = new SelectList(_context.Roles, "RoleId", "RoleId", usersModel.RoleId);
            ViewData["ServiceProviderId"] = new SelectList(_context.ServiceProviders, "ServiceProviderId", "ServiceName", usersModel.ServiceProviderId);
            return View(usersModel);
        }

        // GET: Admin/Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usersModel = await _context.Users.FindAsync(id);
            if (usersModel == null)
            {
                return NotFound();
            }
            ViewData["RechargePlanId"] = new SelectList(_context.RechargePlans, "RechargePlanId", "RechargePlanName", usersModel.RechargePlanId);
            ViewData["RoleId"] = new SelectList(_context.Roles, "RoleId", "RoleId", usersModel.RoleId);
            ViewData["ServiceProviderId"] = new SelectList(_context.ServiceProviders, "ServiceProviderId", "ServiceName", usersModel.ServiceProviderId);
            return View(usersModel);
        }

        // POST: Admin/Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserId,ServiceProviderId,RechargePlanId,UserName,Email,Phone,Password,RoleId")] UsersModel usersModel)
        {
            if (id != usersModel.UserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(usersModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsersModelExists(usersModel.UserId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["RechargePlanId"] = new SelectList(_context.RechargePlans, "RechargePlanId", "RechargePlanName", usersModel.RechargePlanId);
            ViewData["RoleId"] = new SelectList(_context.Roles, "RoleId", "RoleId", usersModel.RoleId);
            ViewData["ServiceProviderId"] = new SelectList(_context.ServiceProviders, "ServiceProviderId", "ServiceName", usersModel.ServiceProviderId);
            return View(usersModel);
        }

        // GET: Admin/Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usersModel = await _context.Users
                .Include(u => u.RechargePlans)
                .Include(u => u.Role)
                .Include(u => u.ServiceProvider)
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (usersModel == null)
            {
                return NotFound();
            }

            return View(usersModel);
        }

        // POST: Admin/Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var usersModel = await _context.Users.FindAsync(id);
            _context.Users.Remove(usersModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsersModelExists(int id)
        {
            return _context.Users.Any(e => e.UserId == id);
        }
    }
}
