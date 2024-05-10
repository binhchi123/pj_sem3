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
    public class RechargeLogsController : Controller
    {
        private readonly OMRContext _context;

        public RechargeLogsController(OMRContext context)
        {
            _context = context;
        }

        // GET: Admin/RechargeLogs
        public async Task<IActionResult> Index()
        {
            return View(await _context.RechargeLogs.ToListAsync());
        }

        // GET: Admin/RechargeLogs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rechargeLogsModel = await _context.RechargeLogs
                .FirstOrDefaultAsync(m => m.RechargeLogId == id);
            if (rechargeLogsModel == null)
            {
                return NotFound();
            }

            return View(rechargeLogsModel);
        }

        // GET: Admin/RechargeLogs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/RechargeLogs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RechargeLogId,RechargePlanId,UserId,RechargeDate")] RechargeLogsModel rechargeLogsModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(rechargeLogsModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(rechargeLogsModel);
        }

        // GET: Admin/RechargeLogs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rechargeLogsModel = await _context.RechargeLogs.FindAsync(id);
            if (rechargeLogsModel == null)
            {
                return NotFound();
            }
            return View(rechargeLogsModel);
        }

        // POST: Admin/RechargeLogs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RechargeLogId,RechargePlanId,UserId,RechargeDate")] RechargeLogsModel rechargeLogsModel)
        {
            if (id != rechargeLogsModel.RechargeLogId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rechargeLogsModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RechargeLogsModelExists(rechargeLogsModel.RechargeLogId))
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
            return View(rechargeLogsModel);
        }

        // GET: Admin/RechargeLogs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rechargeLogsModel = await _context.RechargeLogs
                .FirstOrDefaultAsync(m => m.RechargeLogId == id);
            if (rechargeLogsModel == null)
            {
                return NotFound();
            }

            return View(rechargeLogsModel);
        }

        // POST: Admin/RechargeLogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var rechargeLogsModel = await _context.RechargeLogs.FindAsync(id);
            _context.RechargeLogs.Remove(rechargeLogsModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RechargeLogsModelExists(int id)
        {
            return _context.RechargeLogs.Any(e => e.RechargeLogId == id);
        }
    }
}
