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
using System.Text;
using System.Net.Http;

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
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            // step1: get request
            var response = await BankApi.InitializeClient().GetAsync($"api/customers/{id}");

            if (!response.IsSuccessStatusCode)
                throw new Exception();

            // step 2: Storing the response details recieved from web api; (data type is String)
            var result = response.Content.ReadAsStringAsync().Result;

            // step3: Deserializing the response recieved from web api and storing into a list.
            var customer = JsonConvert.DeserializeObject<CustomerDto>(result);
          
            return View(customer);
        }

        ////GET: MyProfile/Edit
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();
            // step1: get request
            var response = await BankApi.InitializeClient().GetAsync($"api/customers/{id}");

            if (!response.IsSuccessStatusCode)
                throw new Exception();

            // step 2: Storing the response details recieved from web api; (data type is String)
            var result = response.Content.ReadAsStringAsync().Result;

            // step3: Deserializing the response recieved from web api and storing into a list.
            var customer = JsonConvert.DeserializeObject<CustomerDto>(result);
            PopulateStateDropDownList(customer.State);

            return View(customer);
        }


        //[Bind(include: "CustomerName, TFN, Address, City, State, PostCode, Phone")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CustomerDto customer)
        {
            if (id != customer.CustomerID)
                return NotFound();

            if (ModelState.IsValid)
            {
                var content = new StringContent(JsonConvert.SerializeObject(customer), Encoding.UTF8, "application/json");
                var response = BankApi.InitializeClient().PutAsync("api/customers", content).Result;

                

                if (response.IsSuccessStatusCode)
                    return RedirectToAction("Index");
            }
       
            return View(customer);
        }

        // GET: Movies/Delete/1
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var response = await BankApi.InitializeClient().GetAsync($"api/customers/{id}");

            if (!response.IsSuccessStatusCode)
                throw new Exception();

            var result = response.Content.ReadAsStringAsync().Result;
            var customer = JsonConvert.DeserializeObject<CustomerDto>(result);

            return View(customer);
        }

        // POST: Movies/Delete/1
        [HttpPost]
        [ActionName("Delete")]
        // [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var response = BankApi.InitializeClient().DeleteAsync($"api/customers/{id}").Result;

            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            return NotFound();
        }

        // state select list
        private void PopulateStateDropDownList(object selectedState = null)
        {

            var States = Enum.GetValues(typeof(State))
                .Cast<State>()
                .Select(x => new SelectListItem { Text = x.ToString(), Value = ((int)x).ToString() })
                .ToList();

            // value == state.code in int
            // text == state.string code
            // value -> text
            // selectedState == default selected state
             ViewBag.States = new SelectList(States, "Value", "Text", selectedState);
        }

    }
}
