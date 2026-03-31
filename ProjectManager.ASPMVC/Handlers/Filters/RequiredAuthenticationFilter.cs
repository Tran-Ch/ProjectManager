using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ProjectManager.ASPMVC.Handlers.Filters
{
    public class RequiredAuthenticationFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            UserSessionManager sessionManager = context.HttpContext.RequestServices.GetService<UserSessionManager>();
            if (!sessionManager.IsAuthenticated())
            {
                context.Result = new RedirectToActionResult("Login", "Account", null);
            }

            base.OnActionExecuting(context);
        }
    }
}

