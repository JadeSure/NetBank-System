using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using a2_s3736719_s3677615.Data;
using a2_s3736719_s3677615.Models;
using a2_s3736719_s3677615.Attributes;

namespace a2_s3736719_s3677615.Controllers
{
    // protection
    [AuthorizeCustomer]
    public class BillPaysController : Controller
    {
        private readonly NwbaDbContext _context;

        public BillPaysController(NwbaDbContext context)
        {
            _context = context;
        }

        // GET: BillPays
        public async Task<IActionResult> Index()
        {
            var nwbaDbContext = _context.BillPays.Include(b => b.Account).Include(b => b.Payee);
            return View(await nwbaDbContext.ToListAsync());
        }

        // GET: BillPays/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var billPay = await _context.BillPays
                .Include(b => b.Account)
                .Include(b => b.Payee)
                .FirstOrDefaultAsync(m => m.BillPayID == id);
            if (billPay == null)
            {
                return NotFound();
            }

            return View(billPay);
        }

        // GET: BillPays/Create
        public IActionResult Create()
        {
            ViewData["AccountNumber"] = new SelectList(_context.Accounts, "AccountNumber", "AccountNumber");
            ViewData["PayeeID"] = new SelectList(_context.Payees, "PayeeID", "PayeeName");
            ViewData["Period"] = new SelectList(Enum.GetValues(typeof(Period)));
            return View();
        }

        // POST: BillPays/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BillPayID,AccountNumber,PayeeID,Amount,ScheduleDate,Period")] BillPay billPay)
        {
          
            billPay.ScheduleDate = DateTime.SpecifyKind(billPay.ScheduleDate, DateTimeKind.Local).ToUniversalTime();
            billPay.BillPayStatus = BillPayStatus.Ready;
            

            if (billPay.ScheduleDate <= DateTime.UtcNow)
            {
                ModelState.AddModelError(nameof(billPay.ScheduleDate), "Schedula date must be over that right now date");
            }

            if (ModelState.IsValid)
            {
                billPay.ModifyDate = DateTime.UtcNow;
                _context.Add(billPay);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["AccountNumber"] = new SelectList(_context.Accounts, "AccountNumber", "AccountNumber", billPay.AccountNumber);
            ViewData["PayeeID"] = new SelectList(_context.Payees, "PayeeID", "PayeeName", billPay.PayeeID);
            ViewData["Period"] = new SelectList(Enum.GetValues(typeof(Period)));
            return View(billPay);
        }

        // GET: BillPays/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var billPay = await _context.BillPays.FindAsync(id);
            if (billPay == null)
            {
                return NotFound();
            }
            ViewData["AccountNumber"] = new SelectList(_context.Accounts, "AccountNumber", "AccountNumber", billPay.AccountNumber);
            ViewData["PayeeID"] = new SelectList(_context.Payees, "PayeeID", "PayeeName", billPay.PayeeID);
            ViewData["Period"] = new SelectList(Enum.GetValues(typeof(Period)));
            return View(billPay);
        }

        // POST: BillPays/Edit/5
      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BillPayID,AccountNumber,PayeeID,Amount,ScheduleDate,Period,ModifyDate")] BillPay billPay)
        {

            billPay.ScheduleDate = DateTime.SpecifyKind(billPay.ScheduleDate, DateTimeKind.Local).ToUniversalTime();
            billPay.BillPayStatus = BillPayStatus.Ready;

            if (billPay.ScheduleDate <= DateTime.UtcNow)
            {
                ModelState.AddModelError(nameof(billPay.ScheduleDate), "Schedula date must be over that right now date");
            }


            if (id != billPay.BillPayID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    billPay.ModifyDate = DateTime.UtcNow;
                    _context.Update(billPay);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BillPayExists(billPay.BillPayID))
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
            ViewData["AccountNumber"] = new SelectList(_context.Accounts, "AccountNumber", "AccountNumber", billPay.AccountNumber);
            ViewData["PayeeID"] = new SelectList(_context.Payees, "PayeeID", "PayeeName", billPay.PayeeID);
            ViewData["Period"] = new SelectList(Enum.GetValues(typeof(Period)));
            return View(billPay);
        }

        // GET: BillPays/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var billPay = await _context.BillPays
                .Include(b => b.Account)
                .Include(b => b.Payee)
                .FirstOrDefaultAsync(m => m.BillPayID == id);
            if (billPay == null)
            {
                return NotFound();
            }

            return View(billPay);
        }

        // POST: BillPays/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var billPay = await _context.BillPays.FindAsync(id);
            _context.BillPays.Remove(billPay);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BillPayExists(int id)
        {
            return _context.BillPays.Any(e => e.BillPayID == id);
        }
    }
}
