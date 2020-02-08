using SimpleHashing;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using a2_s3736719_s3677615.Data;
using a2_s3736719_s3677615.Models;
using System;
using System.Linq;
using a2_s3736719_s3677615.Utilities;


// adjustive used week 7 tutorial example
namespace a2_s3736719_s3677615.Controllers
{
    // customized routing, hidden real link
    [Route("/Nwba/SecureLogin")]
    public class LoginController : Controller
    {
        private readonly NwbaDbContext _context;

        public LoginController(NwbaDbContext context) => _context = context;

        // default page by get, only in url XX/Login, this method will be call
        [HttpGet]
        public IActionResult Login() => View();


        // post info to login after clicking login button by post function
        [HttpPost]
        public async Task<IActionResult> Login(string userID, string password)
        {

            //var login = _context.Logins.FirstOrDefault(x => userID == x.UserID);
            //var response = await BankApi.InitializeClient().GetAsync("api/logins");

            //if (!response.IsSuccessStatusCode)
            //    throw new Exception();

            //// step 2: Storing the response details recieved from web api; (data type is String)
            //var result = response.Content.ReadAsStringAsync().Result;

            //// step3: Deserializing the response recieved from web api and storing into a list.
            //var loginList = JsonConvert.DeserializeObject<List<Login>>(result);

            //var login = loginList.FirstOrDefault(x => userID == x.UserID);


            // Find the user id in database
            var login = _context.Logins.FirstOrDefault(x => x.UserID == userID);

            // Check if the account exist
            if (login == null)
            {
                ModelState.AddModelError("LoginFailed", "Invalid user ID, please try again.");
                return View(new Login { UserID = userID });
            }

            DateTime loginTime = DateTime.UtcNow;

            try
            {
                login.AttemptLogin(password, loginTime);

            }
            catch (InvalidOperationException ioe)
            {
                ModelState.AddModelError("LoginFailed", ioe.Message);
            }

            await _context.SaveChangesAsync();

            if (!ModelState.IsValid)
            {
                return View(new Login { UserID = userID });
            }

            // Reset the attempt count after success login
            login.UnlockUser(loginTime);
            await _context.SaveChangesAsync();

            // Send the success login details
            HttpContext.Session.SetInt32(nameof(Customer.CustomerID), login.CustomerID);
            HttpContext.Session.SetString(nameof(Customer.CustomerName), login.Customer.CustomerName);

            // Redirect action to ATM controller and MakeTransaction method
            return RedirectToAction("MakeTransaction", "ATM");
        }

        // customized routing
        [Route("LogoutNow")]
        public IActionResult Logout()
        {
            // Logout customer, clear the session
            HttpContext.Session.Clear();

            return RedirectToAction("Index", "Home");
        }
    }
}