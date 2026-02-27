using System;
using System.Web;
using System.Web.Mvc;

namespace TaskTracker.Filters // Namespace alag hona chahiye
{
    public class JwtAuthorizeAttribute : ActionFilterAttribute // Class ka naam 'Attribute' suffix ke saath rakhein
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            // Cookie se token nikalna
            var cookie = filterContext.HttpContext.Request.Cookies["jwt"];

            if (cookie == null || string.IsNullOrEmpty(cookie.Value))
            {
                // Agar token nahi hai to Login par bhej do
                filterContext.Result = new RedirectResult("/Account/Login");
            }

            // Mazeed security ke liye yahan Token Validate karne ka logic bhi aa sakta hai

            base.OnActionExecuting(filterContext);
        }
    }
}