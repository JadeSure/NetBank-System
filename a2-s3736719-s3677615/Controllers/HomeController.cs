using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using a2_s3736719_s3677615.Models;
using a2_s3736719_s3677615.Data;
using Microsoft.EntityFrameworkCore;
using a2_s3736719_s3677615.ViewModels;
using a2_s3736719_s3677615.Utilities;

namespace a2_s3736719_s3677615.Controllers
{
    // the first login pagging
    public class HomeController : Controller
    {
        public IActionResult Index() => View();

        public IActionResult Privacy() => View();

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() =>
            View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
      
        }
    }

