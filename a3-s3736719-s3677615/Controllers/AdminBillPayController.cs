using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using a3_s3736719_s3677615.Models;
using a3_s3736719_s3677615.Helper;
using System.Net.Http;
using System.Text;
using a3_s3736719_s3677615.Attributes;

namespace a3_s3736719_s3677615.Controllers
{
    // protection
    [AuthorizeCustomer]
    public class AdminBillPayController : Controller
    {
        [Route("/Admin/SecureBillPay")]
        public async Task<IActionResult> Index()
        {
            // step1: get request
            var response = await BankApi.InitializeClient().GetAsync("api/billpays");

            if (!response.IsSuccessStatusCode)
                throw new Exception();

            // step 2: Storing the response details recieved from web api; (data type is String)
            var result = response.Content.ReadAsStringAsync().Result;

            // step3: Deserializing the response recieved from web api and storing into a list.
            var logins = JsonConvert.DeserializeObject<List<BillPayDto>>(result);

            return View(logins);
        }

        // lock user login status
        // [HttpPost]
        // [ValidateAntiForgeryToken]
        [Route("/Admin/SecureBlock")]
        public async Task<IActionResult> Block(int id)
        {
            // step1: get request
            var response = await BankApi.InitializeClient().GetAsync($"api/billpays/{id}");

            if (!response.IsSuccessStatusCode)
                throw new Exception();

            // step 2: Storing the response details recieved from web api; (data type is String)
            var result = response.Content.ReadAsStringAsync().Result;

            // step3: Deserializing the response recieved from web api and storing into a list.
            var billPay = JsonConvert.DeserializeObject<BillPayDto>(result);

            billPay.BillPayStatus = BillPayStatus.Blocked;
            billPay.ModifyDate = DateTime.UtcNow;

            if (ModelState.IsValid)
            {
                var content = new StringContent(JsonConvert.SerializeObject(billPay), Encoding.UTF8, "application/json");
                var responsePut = BankApi.InitializeClient().PutAsync("api/billpays", content).Result;

                if (responsePut.IsSuccessStatusCode)
                    return RedirectToAction("Index");
            }

            return View(billPay);

        }

        // unlocked user login status
        // [HttpPost]
        // [ValidateAntiForgeryToken]
        [Route("/Admin/SecureUnBlock")]
        public async Task<IActionResult> UnBlock(int id)
        {
            // step1: get request
            var response = await BankApi.InitializeClient().GetAsync($"api/billpays/{id}");

            if (!response.IsSuccessStatusCode)
                throw new Exception();

            // step 2: Storing the response details recieved from web api; (data type is String)
            var result = response.Content.ReadAsStringAsync().Result;

            // step3: Deserializing the response recieved from web api and storing into a list.
            var billPay = JsonConvert.DeserializeObject<BillPayDto>(result);

            billPay.BillPayStatus = BillPayStatus.Ready;
            billPay.ModifyDate = DateTime.UtcNow;

            if (ModelState.IsValid)
            {
                var content = new StringContent(JsonConvert.SerializeObject(billPay), Encoding.UTF8, "application/json");
                var responsePut = BankApi.InitializeClient().PutAsync("api/billpays", content).Result;

                if (responsePut.IsSuccessStatusCode)
                    return RedirectToAction("Index");
            }

            return View(billPay);

        }
    }
}
