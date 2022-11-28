using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace EntrenatePues.Web.Middlewares
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        /// <summary>
        /// Validate if the context exists a user, in case it does not exist returns unauthorized
        /// </summary>
        /// <param name="context"></param>
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            object user = context.HttpContext.Items["User"];
            if (user == null)
            {
                // not logged in
                context.Result = new JsonResult(new
                 {
                     status = StatusCodes.Status401Unauthorized,
                     message = "Unauthorized"

                 })
                { StatusCode = StatusCodes.Status401Unauthorized };
            }
        }
    }
}
