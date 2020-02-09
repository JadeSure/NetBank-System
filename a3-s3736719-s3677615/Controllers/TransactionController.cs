using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using a3_s3736719_s3677615.Helper;
using a3_s3736719_s3677615.Models;
using a3_s3736719_s3677615.Utilities;
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
            await PopulateDropDownListAsync();
            return View(new SearchHistoryViewModel());
        }

        // POST: Transaction/SearchHistory
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SearchHistory(SearchHistoryViewModel searchRequest)
        {

            if (ModelState.IsValid)
            {
                // Transfer the local time into utc time
                if (searchRequest.StartTime.HasValue)
                {
                    searchRequest.StartTime = DateTime.SpecifyKind((DateTime)searchRequest.StartTime, DateTimeKind.Local).ToUniversalTime();
                }

                if (searchRequest.EndTime.HasValue)
                {
                    searchRequest.EndTime = DateTime.SpecifyKind((DateTime)searchRequest.EndTime, DateTimeKind.Local).ToUniversalTime();
                }

                // Request data through web api
                var content = new StringContent(JsonConvert.SerializeObject(searchRequest), Encoding.UTF8, "application/json");
                var response = await BankApi.InitializeClient().PostAsync("api/transactions/search", content);

                // Read the result and send into session
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    HttpContext.Session.SetInt32("searchRequestAccount", searchRequest.AccountNumber ?? 0);
                    HttpContext.Session.SetString("searchResult", result);
                    return RedirectToAction("Index");
                }
            }

            // Return to view if the validation failed
            await PopulateDropDownListAsync(searchRequest.CustomerID, searchRequest.AccountNumber);
            return View(searchRequest);
        }


        public IActionResult Index()
        {

            // Get the response details from session
            var result = HttpContext.Session.GetString("searchResult");
            var transactions = JsonConvert.DeserializeObject<List<TransactionDto>>(result);

            return View(transactions);
        }

        public IActionResult SearchPieChart()
        {
            // Get the response details from session
            var result = HttpContext.Session.GetString("searchResult");
            var transactions = JsonConvert.DeserializeObject<List<TransactionDto>>(result);

            // Accumulate the amount by transaction type
            Dictionary<string, decimal> chartData = new Dictionary<string, decimal>();
            foreach (TransactionDto t in transactions)
            {
                if (!chartData.ContainsKey(t.TransactionType.GetDisplayName()))
                {
                    chartData.Add(t.TransactionType.GetDisplayName(), t.Amount);
                }

                chartData[t.TransactionType.GetDisplayName()] += t.Amount;
            }

            // Generate the data list for chart
            List<string> keyList = new List<string>(chartData.Keys);
            List<decimal> valueList = new List<decimal>(chartData.Values);
            ViewBag.ChartLabel = JsonConvert.SerializeObject(keyList);
            ViewBag.ChartValue = JsonConvert.SerializeObject(valueList);

            return View();
        }

        public IActionResult SearchBarChart()
        {
            // Get the response details from session
            var result = HttpContext.Session.GetString("searchResult");
            var account = HttpContext.Session.GetInt32("searchRequestAccount");
            var transactions = JsonConvert.DeserializeObject<List<TransactionDto>>(result);

            // Accumulate the amount by account number
            Dictionary<string, Dictionary<string, decimal>> chartData = new Dictionary<string, Dictionary<string, decimal>>();
            foreach (TransactionDto t in transactions)
            {
                var accountNumber = t.AccountNumber.ToString();
                var destinationAccountNumber = t.DestinationAccountNumber.ToString();

                if (!chartData.ContainsKey(accountNumber))
                {
                    chartData.Add(accountNumber, new Dictionary<string, decimal>());
                    chartData[accountNumber].Add("Revenue", 0);
                    chartData[accountNumber].Add("Expense", 0);
                }

                if (destinationAccountNumber != "" && !chartData.ContainsKey(destinationAccountNumber))
                {
                    chartData.Add(destinationAccountNumber, new Dictionary<string, decimal>());
                    chartData[destinationAccountNumber].Add("Revenue", 0);
                    chartData[destinationAccountNumber].Add("Expense", 0);
                }

                // Revenue
                if (t.TransactionType == TransactionType.D)
                {
                    chartData[accountNumber]["Revenue"] += t.Amount;
                }

                // Expense
                if (t.TransactionType == TransactionType.W
                    || t.TransactionType == TransactionType.S
                    || t.TransactionType == TransactionType.B)
                {
                    chartData[accountNumber]["Expense"] -= t.Amount;
                }

                // Revenue/Expense
                if (t.TransactionType == TransactionType.T)
                {
                    chartData[accountNumber]["Expense"] -= t.Amount;
                    chartData[destinationAccountNumber]["Revenue"] += t.Amount;
                }

            }

            // Generate the data list for chart
            var groupList = new List<string>();
            var revenueList = new List<decimal>();
            var expenseList = new List<decimal>();

            foreach (var group in chartData)
            {
                groupList.Add(group.Key);

                foreach (var data in group.Value)
                {
                    if (data.Key.Equals("Revenue"))
                    {
                        revenueList.Add(data.Value);
                    }
                    else
                    {
                        expenseList.Add(data.Value);
                    }
                }
            }

            ViewBag.ChartGroup = JsonConvert.SerializeObject(groupList);
            ViewBag.ChartRevenue = JsonConvert.SerializeObject(revenueList);
            ViewBag.ChartExpense = JsonConvert.SerializeObject(expenseList);

            return View();
        }

        public IActionResult SearchLineChart()
        {
            // Get the response details from session
            var result = HttpContext.Session.GetString("searchResult");
            var account = HttpContext.Session.GetInt32("searchRequestAccount");
            var transactions = JsonConvert.DeserializeObject<List<TransactionDto>>(result);

            // Accumulate the amount by year month
            Dictionary<string, Dictionary<string, decimal>> chartData = new Dictionary<string, Dictionary<string, decimal>>();
            foreach (TransactionDto t in transactions)
            {
                var yearMonth = t.ModifyDate.ToLocalTime().Year.ToString() + "/" + t.ModifyDate.ToLocalTime().Month.ToString();

                if (!chartData.ContainsKey(yearMonth))
                {
                    chartData.Add(yearMonth, new Dictionary<string, decimal>());
                    chartData[yearMonth].Add("Revenue", 0);
                    chartData[yearMonth].Add("Expense", 0);
                }

                if (t.TransactionType == TransactionType.D
                    || (t.DestinationAccountNumber == account
                            && t.TransactionType == TransactionType.T))
                {
                    chartData[yearMonth]["Revenue"] += t.Amount;
                }

                if (t.TransactionType == TransactionType.W
                    || t.TransactionType == TransactionType.S
                    || t.TransactionType == TransactionType.B
                    || (t.AccountNumber == account
                            && t.TransactionType == TransactionType.T))
                {
                    chartData[yearMonth]["Expense"] += t.Amount;
                }
            }

            var groupList = new List<string>();
            var revenueList = new List<decimal>();
            var expenseList = new List<decimal>();

            foreach (var group in chartData)
            {
                groupList.Add(group.Key);

                foreach (var data in group.Value)
                {
                    if (data.Key.Equals("Revenue"))
                    {
                        revenueList.Add(data.Value);
                    }
                    else
                    {
                        expenseList.Add(data.Value);
                    }
                }
            }

            ViewBag.ChartGroup = JsonConvert.SerializeObject(groupList);
            ViewBag.ChartRevenue = JsonConvert.SerializeObject(revenueList);
            ViewBag.ChartExpense = JsonConvert.SerializeObject(expenseList);

            return View();
        }


        private async Task PopulateDropDownListAsync(object selectedCustomer = null, object selectedAccount = null)
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

            ViewData["CustomersList"] = new SelectList(customers, "CustomerID", "CustomerID", selectedCustomer);
            ViewData["AccountsList"] = new SelectList(accounts, "AccountNumber", "AccountNumber", selectedAccount);
        }

    }
}