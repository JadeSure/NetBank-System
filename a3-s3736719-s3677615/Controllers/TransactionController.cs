using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using a3_s3736719_s3677615.Helper;
using a3_s3736719_s3677615.Models;
using a3_s3736719_s3677615.ViewModels;
using Microsoft.AspNetCore.Http;
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

        // GET: Transaction/SearchHistory
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SearchHistory(SearchHistoryViewModel searchRequest)
        {

            if (ModelState.IsValid)
            {
                if (searchRequest.StartTime.HasValue)
                {
                    searchRequest.StartTime = DateTime.SpecifyKind((DateTime)searchRequest.StartTime, DateTimeKind.Local).ToUniversalTime();
                }

                if (searchRequest.EndTime.HasValue)
                {
                    searchRequest.EndTime = DateTime.SpecifyKind((DateTime)searchRequest.EndTime, DateTimeKind.Local).ToUniversalTime();
                }

                var content = new StringContent(JsonConvert.SerializeObject(searchRequest), Encoding.UTF8, "application/json");
                var response = await BankApi.InitializeClient().PostAsync("api/transactions/search", content);

                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    HttpContext.Session.SetString("searchResult", result);
                    return RedirectToAction("Index");
                }
            }
            return View(searchRequest);
        }


        public IActionResult Index()
        {

            //var response = await BankApi.InitializeClient().GetAsync("api/transactions");

            // Storing the response details recieved from web api.
            var result = HttpContext.Session.GetString("searchResult");
            // Deserializing the response recieved from web api and storing into a list
            var transactions = JsonConvert.DeserializeObject<List<TransactionDto>>(result);

            return View(transactions);
        } 

    }
}