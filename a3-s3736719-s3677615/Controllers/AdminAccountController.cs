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
    public class AdminAccountController : Controller
    {
        [Route("/Admin/SecureAccount")]
        public async Task<IActionResult> Index()
        {
            // step1: get request
            var response = await BankApi.InitializeClient().GetAsync("api/logins");

            if (!response.IsSuccessStatusCode)
                throw new Exception();

            // step 2: Storing the response details recieved from web api; (data type is String)
            var result = response.Content.ReadAsStringAsync().Result;

            // step3: Deserializing the response recieved from web api and storing into a list.
            var logins = JsonConvert.DeserializeObject<List<LoginDto>>(result);

            return View(logins);
        }

        // lock user login status
        // [HttpPost]
        // [ValidateAntiForgeryToken]
        [Route("/Admin/SecureLock")]
        public async Task<IActionResult> Lock(int id)
        {
            // step1: get request
            var response = await BankApi.InitializeClient().GetAsync($"api/logins/{id}");

            if (!response.IsSuccessStatusCode)
                throw new Exception();

            // step 2: Storing the response details recieved from web api; (data type is String)
            var result = response.Content.ReadAsStringAsync().Result;

            // step3: Deserializing the response recieved from web api and storing into a list.
            var login = JsonConvert.DeserializeObject<LoginDto>(result);

            login.LoginStatus = LoginStatus.Locked;
            login.ModifyDate = DateTime.UtcNow;

            if (ModelState.IsValid)
            {
                var content = new StringContent(JsonConvert.SerializeObject(login), Encoding.UTF8, "application/json");
                var responsePut = BankApi.InitializeClient().PutAsync("api/logins", content).Result;

                if (responsePut.IsSuccessStatusCode)
                    return RedirectToAction("Index");
            }

            return View(login);

        }

        // unlocked user login status
       
        // [ValidateAntiForgeryToken]
        [Route("/Admin/SecureUnLock")]
        public async Task<IActionResult> UnLock(int id)
        {
            // step1: get request
            var response = await BankApi.InitializeClient().GetAsync($"api/logins/{id}");

            if (!response.IsSuccessStatusCode)
                throw new Exception();

            // step 2: Storing the response details recieved from web api; (data type is String)
            var result = response.Content.ReadAsStringAsync().Result;

            // step3: Deserializing the response recieved from web api and storing into a list.
            var login = JsonConvert.DeserializeObject<LoginDto>(result);

            login.LoginStatus = LoginStatus.Active;
            login.ModifyDate = DateTime.UtcNow;

            if (ModelState.IsValid)
            {
                var content = new StringContent(JsonConvert.SerializeObject(login), Encoding.UTF8, "application/json");
                var responsePut = BankApi.InitializeClient().PutAsync("api/logins", content).Result;

                if (responsePut.IsSuccessStatusCode)
                    return RedirectToAction("Index");
            }

            return View(login);

        }
    }
}
