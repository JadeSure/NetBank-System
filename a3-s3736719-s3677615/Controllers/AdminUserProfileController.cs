using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using a3_s3736719_s3677615.Attributes;
using a3_s3736719_s3677615.Helper;
using a3_s3736719_s3677615.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SimpleHashing;

namespace a3_s3736719_s3677615.Controllers
{
    // protection
    [AuthorizeCustomer]
    public class AdminUserProfileController : Controller
    {

        // public AdminUserProfileController(NwbaDbContext context) => _context = context;

        public async Task<IActionResult> Index()
        {
            // step1: get request
            var response = await BankApi.InitializeClient().GetAsync("api/customers");

            if (!response.IsSuccessStatusCode)
                throw new Exception();

            // step 2: Storing the response details recieved from web api; (data type is String)
            var result = response.Content.ReadAsStringAsync().Result;

            // step3: Deserializing the response recieved from web api and storing into a list.
            var customers = JsonConvert.DeserializeObject<List<CustomerDto>>(result);

            return View(customers);
        }


       


        // GET: MyProfile/Details
        //public async Task<IActionResult> Details()
        //{
        //    // Lazy loading.
        //    var customer = await _context.Customers.FindAsync(CustomerID);

        //    return View(customer);
        //}

        ////GET: MyProfile/Edit
        //public async Task<IActionResult> Edit()
        //{
        //    var customer = await _context.Customers.FindAsync(CustomerID);

        //    PopulateStateDropDownList(customer.State);

        //    return View(customer);
        //}


        ////[Bind(include: "CustomerName, TFN, Address, City, State, PostCode, Phone")]
        //[HttpPost]
        //public async Task<IActionResult> Edit(Customer customer)
        //{
        //    var customerToBeUpdated = await _context.Customers.FindAsync(CustomerID);

        //    if (await TryUpdateModelAsync<Customer>(customerToBeUpdated, "",
        //       c => c.CustomerName,
        //       c => c.TFN,
        //       c => c.Address,
        //       c => c.City,
        //       c => c.State,
        //       c => c.PostCode,
        //       c => c.Phone))
        //    {
        //        try
        //        {
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateException /* ex */)
        //        {
        //            //Log the error (uncomment ex variable name and write a log.)
        //            ModelState.AddModelError("", "Unable to save changes. " +
        //                "Try again, and if the problem persists, " +
        //                "see your system administrator.");
        //        }
        //        return RedirectToAction(nameof(Details));
        //    }

        //    return View(customer);
        //}


        //private void PopulateStateDropDownList(object selectedState = null)
        //{

        //    var States = Enum.GetValues(typeof(State))
        //        .Cast<State>()
        //        .Select(x => new SelectListItem { Text = x.ToString(), Value = ((int)x).ToString() })
        //        .ToList();

        //    ViewBag.States = new SelectList(States, "Value", "Text", selectedState);
        //}

        ////GET: MyProfile/ChangePassword
        //public async Task<IActionResult> ChangePassword()
        //{

        //    return View(new PasswordViewModel
        //    {
        //        Login = await _context.Logins.FindAsync(CustomerID)
        //    });
        //}

        //// change password function
        //[HttpPost]
        //public async Task<IActionResult> ChangePassword(PasswordViewModel model)
        //{
        //    model.Login = await _context.Logins.FindAsync(model.Login.CustomerID);

        //    try
        //    {
        //        model.Login.ChangePassword(model.OldPassword, model.NewPassword);
        //    }
        //    catch (ArgumentOutOfRangeException ae)
        //    {
        //        ModelState.AddModelError("", ae.Message);
        //        return View(model);
        //    }
        //    catch (InvalidOperationException ie)
        //    {
        //        ModelState.AddModelError("", ie.Message);
        //        return View(model);
        //    }

        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Details));
        //}

    }
}
