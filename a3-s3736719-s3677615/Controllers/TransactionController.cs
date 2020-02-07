using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using a2.Web.Helper;
using a3_s3736719_s3677615.Models;
using a3_s3736719_s3677615.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace a3_s3736719_s3677615.Controllers
{
    public class TransactionController : Controller
    {
        // GET: Transaction/SearchHistory
        public async Task<IActionResult> SearchHistory()
        {
            var customerResponse = await BankApi.InitializeClient().GetAsync("api/customers");
            var accountResponse = await BankApi.InitializeClient().GetAsync("api/accounts");

            if (!customerResponse.IsSuccessStatusCode || !accountResponse.IsSuccessStatusCode)
            {
                throw new Exception();
            }

            // Storing the response details recieved from web api.
            var customerResult = customerResponse.Content.ReadAsStringAsync().Result;
            var accountResult = accountResponse.Content.ReadAsStringAsync().Result;

            // Deserializing the response recieved from web api and storing into a list.
            var customers = JsonConvert.DeserializeObject<List<CustomerDto>>(customerResult);
            var accounts = JsonConvert.DeserializeObject<List<AccountDto>>(accountResult);

            ViewData["CustomersList"] = new SelectList(customers, "CustomerID", "CustomerID");
            ViewData["AccountsList"] = new SelectList(accounts, "AccountNumber", "AccountNumber");

            return View(new SearchHistoryViewModel());
        }

        // POST: Transaction/SearchHistory
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> SearchHistory(SearchHistoryViewModel model)
        //{

        // No result
        //return View(model);

        // Has result
        // RedirectToAction("Report");

        //if (ModelState.IsValid)
        //{
        //    var content = new StringContent(JsonConvert.SerializeObject(movie), Encoding.UTF8, "application/json");
        //    var response = MovieApi.InitializeClient().PostAsync("api/movies", content).Result;

        //    if (response.IsSuccessStatusCode)
        //        return RedirectToAction("Index");
        //}

        //return View(movie);

        //}


        //public async Task<IActionResult> Index()
        //{
        //    var response = await BankApi.InitializeClient().GetAsync("api/transactions");

        //    if (!response.IsSuccessStatusCode)
        //        throw new Exception();

        //    // Storing the response details recieved from web api.
        //    var result = response.Content.ReadAsStringAsync().Result;

        //    // Deserializing the response recieved from web api and storing into a list.
        //    var transactions = JsonConvert.DeserializeObject<List<TransactionDto>>(result);

        //    return View(transactions);
        //}

    }
}