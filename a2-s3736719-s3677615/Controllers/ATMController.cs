using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using a2_s3736719_s3677615.Data;
using a2_s3736719_s3677615.Models;
using a2_s3736719_s3677615.Utilities;
using a2_s3736719_s3677615.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using a2_s3736719_s3677615.Attributes;

namespace a2_s3736719_s3677615.Controllers
{

    [AuthorizeCustomer]
    public class ATMController : Controller
    {
        private readonly NwbaDbContext _context;

        // ReSharper disable once PossibleInvalidOperationException
        private int CustomerID => HttpContext.Session.GetInt32(nameof(Customer.CustomerID)).Value;

        public ATMController(NwbaDbContext context) => _context = context;


        // http get
        public async Task<IActionResult> MakeTransaction()
        {
            // Lazy loading.
            // The Customer.Accounts property will be lazy loaded upon demand.
            var customer = await _context.Customers.FindAsync(CustomerID);

            ViewData["AccountNumber"] = new SelectList(customer.Accounts, "AccountNumber", "AccountNumber");
            ViewData["DestAccountNumber"] = new SelectList(_context.Accounts, "AccountNumber", "AccountNumber");
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> MakeTransaction([Bind("TransactionType,AccountNumber,DestinationAccountNumber,Amount,Comment")] Transaction transaction)
        {
            try
            {
                transaction.Account = await _context.Accounts.FindAsync(transaction.AccountNumber);

                switch (transaction.TransactionType)
                {
                    // action for deposit
                    case TransactionType.D:
                        transaction.Account.MakeDeposit(transaction.Amount, transaction.Comment);
                        break;
                    // action for withdraw
                    case TransactionType.W:
                        transaction.Account.MakeWithdrawal(transaction.Amount, transaction.Comment);
                        break;
                    // action for transfer money
                    case TransactionType.T:
                        transaction.DestAccount = await _context.Accounts.FindAsync(transaction.DestinationAccountNumber);
                        transaction.Account.MakeTransfer(transaction.Amount, transaction.DestAccount, transaction.Comment);
                        break;
                    // if the input action is not belong to above, show error messages
                    default:
                        ModelState.AddModelError(nameof(transaction.TransactionType), "Incorect transaction type");
                        break;
                }
            }
            catch (ArgumentOutOfRangeException ae)
            {
                ModelState.AddModelError(nameof(transaction.Amount), ae.Message);
            }
            catch (InvalidOperationException ie)
            {
                ModelState.AddModelError("", ie.Message);
            }

            if (!ModelState.IsValid)
            {
                var customer = await _context.Customers.FindAsync(CustomerID);
                ViewData["AccountNumber"] = new SelectList(customer.Accounts, "AccountNumber", "AccountNumber", transaction.AccountNumber);
                ViewData["DestAccountNumber"] = new SelectList(_context.Accounts, "AccountNumber", "AccountNumber", transaction.DestinationAccountNumber);
                //ViewData["TransactionType"] = new SelectList(Enum.GetValues(typeof(TransactionType)), transaction.TransactionType);
                return View(transaction);
            }

            await _context.SaveChangesAsync();
            // redirect action to Mystatement controller and index method
            return RedirectToAction("Index", "Mystatement");
        }

    }
}
