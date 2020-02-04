using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;


// a middleware is created
namespace CustomError_Demo.Controllers
{
    public class StatusCodeController : Controller
    {
        // GET: /<controller>/
        // exception just return status code
        [HttpGet("/StatusCode/{statusCode}")]
        public IActionResult Index(int statusCode)
        {
            return View(statusCode);
        }
    }
}
