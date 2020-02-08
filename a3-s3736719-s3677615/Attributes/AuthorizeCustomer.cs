using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using a3_s3736719_s3677615.Models;

namespace a3_s3736719_s3677615.Attributes
{
    // add authorized info
    public class AuthorizeCustomerAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
           
            var customerID = context.HttpContext.Session.GetInt32(nameof(AdminLogin.AdminId));
            if(!customerID.HasValue)
            {
                // when user try to access a web without login, show html in Index and Home level
                context.Result = new RedirectToActionResult("Login", "AdminLogin", null);
            }
        }
    }
}
