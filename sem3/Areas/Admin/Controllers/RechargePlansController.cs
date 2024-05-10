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
    public class RechargePlansController : Controller
    {
        private readonly OMRContext _context;

        public RechargePlansController(OMRContext context)
        {
            _context = context;
        }

        // GET: Admin/RechargePlans
        public async Task<IActionResult> Index()
        {
            var oMRContext = _context.RechargePlans.Include(r => r.ServiceProvider);
            return View(await oMRContext.ToListAsync());
        }

        // GET: Admin/RechargePlans/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
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

        // GET: Admin/RechargePlans/Create
        public IActionResult Create()
        {
            ViewData["ServiceProviderId"] = new SelectList(_context.ServiceProviders, "ServiceProviderId", "ServiceName");
            return View();
        }

        // POST: Admin/RechargePlans/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RechargePlanId,ServiceProviderId,RechargePlanName,RechargePlanValidity,RechargePlanPrice,RechargePlanData")] RechargePlansModel rechargePlansModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(rechargePlansModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ServiceProviderId"] = new SelectList(_context.ServiceProviders, "ServiceProviderId", "ServiceName", rechargePlansModel.ServiceProviderId);
            return View(rechargePlansModel);
        }

        // GET: Admin/RechargePlans/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rechargePlansModel = await _context.RechargePlans.FindAsync(id);
            if (rechargePlansModel == null)
            {
                return NotFound();
            }
            ViewData["ServiceProviderId"] = new SelectList(_context.ServiceProviders, "ServiceProviderId", "ServiceName", rechargePlansModel.ServiceProviderId);
            return View(rechargePlansModel);
        }

        // POST: Admin/RechargePlans/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RechargePlanId,ServiceProviderId,RechargePlanName,RechargePlanValidity,RechargePlanPrice,RechargePlanData")] RechargePlansModel rechargePlansModel)
        {
            if (id != rechargePlansModel.RechargePlanId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rechargePlansModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RechargePlansModelExists(rechargePlansModel.RechargePlanId))
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
            ViewData["ServiceProviderId"] = new SelectList(_context.ServiceProviders, "ServiceProviderId", "ServiceName", rechargePlansModel.ServiceProviderId);
            return View(rechargePlansModel);
        }

        // GET: Admin/RechargePlans/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
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

        // POST: Admin/RechargePlans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var rechargePlansModel = await _context.RechargePlans.FindAsync(id);
            _context.RechargePlans.Remove(rechargePlansModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RechargePlansModelExists(int id)
        {
            return _context.RechargePlans.Any(e => e.RechargePlanId == id);
        }
    }
}
