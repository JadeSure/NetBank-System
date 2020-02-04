using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Data;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillPaysController : ControllerBase
    {
        private readonly NwbaDbContext _context;

        public BillPaysController(NwbaDbContext context)
        {
            _context = context;
        }

        // GET: api/BillPays
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BillPayAPI>>> GetBillPays()
        {
            return await _context.BillPays.ToListAsync();
        }

        // GET: api/BillPays/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BillPayAPI>> GetBillPay(int id)
        {
            var billPay = await _context.BillPays.FindAsync(id);

            if (billPay == null)
            {
                return NotFound();
            }

            return billPay;
        }

        // PUT: api/BillPays/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBillPay(int id, BillPayAPI billPay)
        {
            if (id != billPay.BillPayID)
            {
                return BadRequest();
            }

            _context.Entry(billPay).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BillPayExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/BillPays
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<BillPayAPI>> PostBillPay(BillPayAPI billPay)
        {
            _context.BillPays.Add(billPay);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBillPay", new { id = billPay.BillPayID }, billPay);
        }

        // DELETE: api/BillPays/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<BillPayAPI>> DeleteBillPay(int id)
        {
            var billPay = await _context.BillPays.FindAsync(id);
            if (billPay == null)
            {
                return NotFound();
            }

            _context.BillPays.Remove(billPay);
            await _context.SaveChangesAsync();

            return billPay;
        }

        private bool BillPayExists(int id)
        {
            return _context.BillPays.Any(e => e.BillPayID == id);
        }
    }
}
