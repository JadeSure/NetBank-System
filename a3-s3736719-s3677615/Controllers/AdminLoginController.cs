using SimpleHashing;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using a2.Web.Helper;
using Newtonsoft.Json;
using System.Collections.Generic;
using System;
using a3_s3736719_s3677615.Models;


// adjustive used week 7 tutorial example
namespace NwbaExample.Controllers
{
    // customized routing, hidden real link
    [Route("/Admin/SecureLogin", Name ="AdminLogin")]
    public class AdminLoginController : Controller
    {
       
        // default page by get, only in url XX/Login, this method will be call
        [HttpGet]
        public IActionResult Login() => View();


        // post info to login after clicking login button by post function
        [HttpPost]
        // public  async Task<IActionResult> Login(string userID, string password)
        public async Task<IActionResult> Login(String userID, string password)
        {
            ViewData["AdminTitle"] = "Admin Login";


            if (!userID.Equals("admin") || !password.Equals("jin"))
            { 
                ModelState.AddModelError("LoginFailed", "Login failed, please try again.");

                // keep login message in the text input box when login failed
                return View(new AdminLogin {AdminId = userID });
            }

            HttpContext.Session.SetString(nameof(AdminLogin.AdminId), userID);
            HttpContext.Session.SetString(nameof(AdminLogin.AdminName), password);

            // to Customer --> index method
            return RedirectToAction("need to be filled");
        }

        // customized routing
        [Route("LogoutNow")]
        public IActionResult Logout()
        {
            // Logout customer, clear the session
            HttpContext.Session.Clear();

            return RedirectToAction("Login");
        }
    }
}
