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
    public class PayeesController : ControllerBase
    {
        private readonly NwbaDbContext _context;

        public PayeesController(NwbaDbContext context)
        {
            _context = context;
        }

        // GET: api/Payees
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PayeeAPI>>> GetPayees()
        {
            return await _context.Payees.ToListAsync();
        }

        // GET: api/Payees/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PayeeAPI>> GetPayee(int id)
        {
            var payee = await _context.Payees.FindAsync(id);

            if (payee == null)
            {
                return NotFound();
            }

            return payee;
        }

        // PUT: api/Payees/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPayee(int id, PayeeAPI payee)
        {
            if (id != payee.PayeeID)
            {
                return BadRequest();
            }

            _context.Entry(payee).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PayeeExists(id))
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

        // POST: api/Payees
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<PayeeAPI>> PostPayee(PayeeAPI payee)
        {
            _context.Payees.Add(payee);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPayee", new { id = payee.PayeeID }, payee);
        }

        // DELETE: api/Payees/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<PayeeAPI>> DeletePayee(int id)
        {
            var payee = await _context.Payees.FindAsync(id);
            if (payee == null)
            {
                return NotFound();
            }

            _context.Payees.Remove(payee);
            await _context.SaveChangesAsync();

            return payee;
        }

        private bool PayeeExists(int id)
        {
            return _context.Payees.Any(e => e.PayeeID == id);
        }
    }
}
