using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using a2_s3736719_s3677615.Data;
using a2_s3736719_s3677615.Models;
using a2_s3736719_s3677615.Utilities;
using a2_s3736719_s3677615.Attributes;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Linq;
using X.PagedList;
using System.Diagnostics;
using System.Collections.Generic;


// adjustive used week 7 tutorial example
namespace NwbaExample.Controllers
{
    // protection
    [AuthorizeCustomer]
    public class MystatementController : Controller
    {
        private readonly NwbaDbContext _context;

        // ReSharper disable once PossibleInvalidOperationException
        private int CustomerID => HttpContext.Session.GetInt32(nameof(Customer.CustomerID)).Value;

        public MystatementController(NwbaDbContext context) => _context = context;


        public async Task<IActionResult> Index()
        {

            // lazy loading
            var customer = await _context.Customers.FindAsync(CustomerID);
            return View(customer);
        }

      
        public async Task<IActionResult> MyStatement(int accountNum, int? page = 1)
        {
            const int pageSize = 4;
            var account = await _context.Accounts.FindAsync(accountNum);
            ViewBag.Account = account;

            // output transaction history with descending order
            var pagedList = await _context.Transactions.
                Where(x => (x.AccountNumber == accountNum || x.DestinationAccountNumber == accountNum)).
                OrderByDescending(s => s.ModifyDate).
                   ToPagedListAsync(page, pageSize);

            return View(pagedList);
        }

        public IActionResult Privacy() => View();

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() =>
            View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}


    

