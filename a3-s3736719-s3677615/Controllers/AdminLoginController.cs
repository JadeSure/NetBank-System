using SimpleHashing;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

using Newtonsoft.Json;
using System.Collections.Generic;
using System;
using a3_s3736719_s3677615.Models;


// adjustive used week 7 tutorial example
namespace a3_s3736719_s3677615.Controllers
{
    // [Route("/Admin/SecureLogin")]
    public class AdminLoginController : Controller
    {
       
        // default page by get, only in url XX/Login, this method will be call
        [HttpGet]
        // customized routing, hidden real link
        
        //[ActionName("Login")]
        public IActionResult Login() => View();


        // post info to login after clicking login button by post function
        [HttpPost]
        public async Task<IActionResult> Login(String AdminId, string AdminName)
        {
            ViewData["Admin Title"] = "Admin Login";

            if (AdminId!="admin" || AdminName!="jin")
            { 
                ModelState.AddModelError("LoginFailed", "Login failed, please try again.");

                // keep login message in the text input box when login failed
                return View(new AdminLogin {AdminId = AdminId });
            }

            HttpContext.Session.SetString(nameof(AdminLogin.AdminId), AdminId);
            HttpContext.Session.SetString(nameof(AdminLogin.AdminName), AdminName);


            // to Customer --> index method
            return RedirectToAction("Index", "AdminUserProfile");
        }

        // customized routing
        [Route("/Admin/LogoutNow")]
        public IActionResult Logout()
        {
            // Logout customer, clear the session
            HttpContext.Session.Clear();

            return RedirectToAction("Login");
        }
    }
}
